using HookUpDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace HookUpDAL.Interfaces
{
    public interface IAccountDAL
    {
        Task<IdentityResult> Register(AppUser appUser, string password);
        Task<bool> CheckUserValid(AppUser appUser, string password);
        Task<bool> UserExists(string username);
        Task<AppUser> GetUser(string username);
        Task<IdentityResult> AddUserRole(AppUser appUser);
    }
}
