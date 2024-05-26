using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL
{
    public class MessageDAL : IMessageDAL
    {
        private readonly DataContext _dataContext;

        public MessageDAL(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddMessage(Message message)
        {
            _dataContext.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _dataContext.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _dataContext.Messages.FindAsync(id);
        }

        public async Task<IQueryable<Message>> GetMessagesForUser()
        {
            return _dataContext.Messages.OrderByDescending(x => x.MessageSent).AsQueryable();
        }

        public async Task<IEnumerable<Message>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _dataContext.Messages
                .Include(s => s.Sender).ThenInclude(p => p.Photos)
                .Include(r => r.Recipient).ThenInclude(p => p.Photos)
                .Where(
                    m => (m.RecipientUsername == currentUsername && m.RecipientDeleted == false && m.SenderUsername == recipientUsername) ||
                        (m.RecipientUsername == recipientUsername && m.SenderDeleted == false && m.SenderUsername == currentUsername)
                )
                .OrderBy(m => m.MessageSent)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }

                await _dataContext.SaveChangesAsync();
            }

            return messages;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
