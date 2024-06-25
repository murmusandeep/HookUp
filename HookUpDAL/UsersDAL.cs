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

        public async Task<IQueryable<AppUser>> GetMemberAsync(string username)
        {
            var query = _dataContext.Users
                 .Where(x => x.UserName == username)
                 .AsQueryable();
            return query;
        }

        public async Task<AppUser> GetUserById(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            return user;
        }

        public async Task<AppUser> GetUserByPhotoId(int photoId)
        {
            var result = await _dataContext.Users
                .Include(p => p.Photos)
                .IgnoreQueryFilters()
                .Where(p => p.Photos.Any(p => p.Id == photoId))
                .FirstOrDefaultAsync();

            return result;
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
