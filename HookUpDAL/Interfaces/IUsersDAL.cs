using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface IUsersDAL
    {
        Task<IQueryable<AppUser>> GetUsers();
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserByUsername(string username);
        Task<IQueryable<AppUser>> GetMemberAsync(string username);
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<AppUser> GetUserByPhotoId(int photoId);
    }
}
