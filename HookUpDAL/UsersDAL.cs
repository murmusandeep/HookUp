using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL
{
    public class UsersDAL : IUsersDAL
    {
        private readonly DataContext _dataContext;

        public UsersDAL(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            return user;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            var user = await _dataContext.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == username);
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _dataContext.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}
