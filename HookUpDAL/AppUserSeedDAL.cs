using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using System.Security.Cryptography;
using System.Text;

namespace HookUpDAL
{
    public class AppUserSeedDAL : IAppUserSeedDAL
    {
        private readonly DataContext _context;

        public AppUserSeedDAL(DataContext context)
        {
            _context = context;
        }
        public void SaveSeedData(List<AppUser> appUsers)
        {
            foreach (var user in appUsers)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                user.PasswordSalt = hmac.Key;

                _context.Users.Add(user);
            }
            _context.SaveChanges();
        }
    }
}
