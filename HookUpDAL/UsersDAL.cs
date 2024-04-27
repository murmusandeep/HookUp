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

        public async Task<IQueryable<AppUser>> GetUsers()
        {
            return _dataContext.Users
                .AsQueryable();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}
