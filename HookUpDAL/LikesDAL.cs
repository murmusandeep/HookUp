using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL
{
    public class LikesDAL : ILikesDAL
    {
        private readonly DataContext _dataContext;

        public LikesDAL(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IQueryable<UserLike>> GetLikes()
        {
            return _dataContext.Likes.AsQueryable();
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await _dataContext.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<IQueryable<AppUser>> GetUsers()
        {
            return _dataContext.Users.OrderBy(u => u.UserName).AsQueryable();
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _dataContext.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
