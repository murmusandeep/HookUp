using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface IAccountDAL
    {
        Task Register(AppUser appUser);
        Task<bool> UserExists(string username);
        Task<AppUser> GetUser(string username);
    }
}
