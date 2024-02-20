using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL
{
    public class AccountDAL : IAccountDAL
    {
        private readonly DataContext _context;
        public AccountDAL(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUser(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task Register(AppUser user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
