using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface IAppUserSeedDAL
    {
        public void SaveSeedData(List<AppUser> appUsers);
    }
}
