using Entities.Dto;
using Entities.Models;

namespace HookUpBLL.Interfaces
{
    public interface IAccountBLL
    {
        Task<User> Register(RegisterDto register);
        Task<bool> UserExists(string username);
        Task<User> GetUser(string username, string password);
    }
}
