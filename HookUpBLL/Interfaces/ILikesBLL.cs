using Entities.Dto;
using Entities.Helpers;

namespace HookUpBLL.Interfaces
{
    public interface ILikesBLL
    {
        public Task<bool> AddLike(string username, int sourceUserId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}
