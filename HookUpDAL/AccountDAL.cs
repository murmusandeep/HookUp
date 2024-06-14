using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL
{
    public class AccountDAL : IAccountDAL
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountDAL(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserRole(AppUser appUser)
        {
            return await _userManager.AddToRoleAsync(appUser, "Member");
        }

        public async Task<bool> CheckUserValid(AppUser appUser, string password)
        {
            var result = await _userManager.CheckPasswordAsync(appUser, password);
            return result;
        }

        public async Task<AppUser> GetUser(string username)
        {
            var result = await _userManager.Users
                .Include(p => p.Photos)
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(x => x.UserName == username.ToLower());
            return result;
        }

        public async Task<IdentityResult> Register(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
