using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities.Dto;
using Entities.Exceptions;
using Entities.Helpers;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;

namespace HookUpBLL
{
    public class MessageBLL : IMessageBLL
    {
        private readonly IUsersDAL _usersDAL;
        private readonly IMessageDAL _messageDAL;
        private readonly IMapper _mapper;

        public MessageBLL(IUsersDAL usersDAL, IMessageDAL messageDAL, IMapper mapper)
        {
            _usersDAL = usersDAL;
            _messageDAL = messageDAL;
            _mapper = mapper;
        }

        public async Task<MessageDto> AddMessage(string username, CreateMessageDto createMessageDto)
        {
            if (username == createMessageDto.RecipientUsername.ToLower())
                throw new BadRequestException("You cannot send message to yourself");

            var sender = await _usersDAL.GetUserByUsername(username);
            var recipent = await _usersDAL.GetUserByUsername(createMessageDto.RecipientUsername);

            if (recipent == null)
                throw new UserNotFoundException(createMessageDto.RecipientUsername);

            var message = new Message
            {
                Sender = sender,
                SenderUsername = sender.UserName,
                Recipient = recipent,
                RecipientUsername = recipent.UserName,
                Content = createMessageDto.Content,
            };

            _messageDAL.AddMessage(message);

            if (await _messageDAL.SaveAllAsync()) return _mapper.Map<MessageDto>(message);

            throw new BadRequestException("Failed to send message");
        }

        public async Task<bool> DeleteMessage(string username, int id)
        {
            var message = await _messageDAL.GetMessage(id);

            if (message.SenderUsername != username && message.RecipientUsername != username)
                throw new UnAuthorizedException("Unauthorize");

            if (message.SenderUsername == username) message.SenderDeleted = true;
            if (message.RecipientUsername == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _messageDAL.DeleteMessage(message);
            }

            return await _messageDAL.SaveAllAsync();
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var result = await _messageDAL.GetMessagesForUser();

            result = messageParams.Container switch
            {
                "Inbox" => result.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => result.Where(u => u.SenderUsername == messageParams.Username && u.SenderDeleted == false),
                _ => result.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
            };

            var messages = result.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string username)
        {
            var result = await _messageDAL.GetMessageThread(currentUsername, username);
            return _mapper.Map<IEnumerable<MessageDto>>(result);
        }
    }
}
