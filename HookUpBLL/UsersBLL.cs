using AutoMapper;
using Entities.Models;
using HookUpBLL.Interfaces;
using HookUpDAL.Interfaces;

namespace HookUpBLL
{
    public class UsersBLL : IUsersBLL
    {
        private readonly IUsersDAL _usersDAL;
        private readonly IMapper _mapper;

        public UsersBLL(IUsersDAL usersDAL, IMapper mapper)
        {
            _usersDAL = usersDAL;
            _mapper = mapper;
        }

        public async Task<User> GetUserById(int id)
        {
            var result = await _usersDAL.GetUserById(id);
            var user = _mapper.Map<User>(result);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var result = await _usersDAL.GetUsers();
            var users = _mapper.Map<IEnumerable<User>>(result);
            return users;
        }
    }
}
