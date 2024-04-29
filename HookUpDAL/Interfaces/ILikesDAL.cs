using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface ILikesDAL
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<IQueryable<AppUser>> GetUsers();
        Task<IQueryable<UserLike>> GetLikes();
    }
}
