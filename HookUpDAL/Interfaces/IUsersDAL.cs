using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface IUsersDAL
    {
        Task<IEnumerable<AppUser>> GetUsers();

        Task<AppUser> GetUserById(int id);
    }
}
