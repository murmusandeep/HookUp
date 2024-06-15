using AutoMapper;
using Entities.Dto;
using HookUpApi.Extensions;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HookUpApi.SignalR
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMessageBLL _messageBLL;
        private readonly IMessageDAL _messageDAL;
        private readonly IUsersDAL _usersDAL;
        private readonly IMapper _mapper;
        private readonly IHubContext<PresenceHub> _presenceHub;

        public MessageHub(IMessageBLL messageBLL, IMessageDAL messageDAL, IUsersDAL usersDAL, IMapper mapper, IHubContext<PresenceHub> presenceHub)
        {
            _messageBLL = messageBLL;
            _messageDAL = messageDAL;
            _usersDAL = usersDAL;
            _mapper = mapper;
            _presenceHub = presenceHub;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"];
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);

            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _messageBLL.GetMessageThread(Context.User.GetUsername(), otherUser);

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var username = Context.User.GetUsername();

            if (username == createMessageDto.RecipientUsername.ToLower()) throw new HubException("You cannot send message to yourself");

            var sender = await _usersDAL.GetUserByUsername(username);
            var recipent = await _usersDAL.GetUserByUsername(createMessageDto.RecipientUsername);

            if (recipent == null) throw new HubException("Not Found user");

            var message = new Message
            {
                Sender = sender,
                SenderUsername = sender.UserName,
                Recipient = recipent,
                RecipientUsername = recipent.UserName,
                Content = createMessageDto.Content,
            };

            var groupName = GetGroupName(sender.UserName, recipent.UserName);

            var group = await _messageDAL.GetMessageGroup(groupName);

            if (group.Connections.Any(x => x.Username == recipent.UserName))
            {
                message.DateRead = DateTime.UtcNow;
            }
            else
            {
                var connections = await PresenceTracker.GetConnectionsForUser(recipent.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageRecieved", new { username = sender.UserName, knownAs = sender.KnownAs });
                }
            }

            _messageDAL.AddMessage(message);

            if (await _messageDAL.SaveAllAsync())
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            }
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;

            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _messageDAL.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

            if (group == null)
            {
                group = new Group(groupName);
                _messageDAL.AddGroup(group);
            }

            group.Connections.Add(connection);

            if (await _messageDAL.SaveAllAsync()) return group;

            throw new HubException("Failed to add to group");
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _messageDAL.GetGroupForConnection(Context.ConnectionId);
            var connection = await _messageDAL.GetConnection(Context.ConnectionId);
            _messageDAL.RemoveConnection(connection);
            if (await _messageDAL.SaveAllAsync()) return group;

            throw new HubException("Failed to remove from group");
        }
    }
}
