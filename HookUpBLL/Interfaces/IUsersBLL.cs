using Entities.Models;

namespace HookUpBLL.Interfaces
{
    public interface IUsersBLL
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
    }
}
