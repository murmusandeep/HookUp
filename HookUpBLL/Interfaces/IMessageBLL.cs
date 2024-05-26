using Entities.Dto;
using Entities.Helpers;

namespace HookUpBLL.Interfaces
{
    public interface IMessageBLL
    {
        Task<MessageDto> AddMessage(string username, CreateMessageDto message);
        Task<bool> DeleteMessage(string username, int id);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string username);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
    }
}
