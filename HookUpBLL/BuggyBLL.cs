using AutoMapper;
using Entities.Models;
using HookUpBLL.Interfaces;
using HookUpDAL.Interfaces;

namespace HookUpBLL
{
    public class BuggyBLL : IBuggyBLL
    {
        private readonly IBuggyDAL _buggyDAL;
        private readonly IMapper _mapper;

        public BuggyBLL(IBuggyDAL buggyDAL, IMapper mapper)
        {
            _buggyDAL = buggyDAL;
            _mapper = mapper;
        }

        public async Task<User> GetUser()
        {
            var result = await _buggyDAL.GetUser();
            var user = _mapper.Map<User>(result);
            return user;
        }
    }
}
