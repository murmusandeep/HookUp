using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface IBuggyDAL
    {
        Task<AppUser> GetUser();
    }
}
