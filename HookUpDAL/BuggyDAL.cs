using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using HookUpDAL.Repository;

namespace HookUpDAL
{
    public class BuggyDAL : IBuggyDAL
    {
        private readonly DataContext _dataContext;

        public BuggyDAL(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AppUser> GetUser()
        {
            return await _dataContext.Users.FindAsync(-1);
        }
    }
}
