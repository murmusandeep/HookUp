using Entities.Models;

namespace HookUpBLL.Interfaces
{
    public interface IBuggyBLL
    {
        Task<User> GetUser();
    }
}
