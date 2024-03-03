using AutoMapper;
using Entities.Dto;
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

        public async Task<MemberDto> GetUserById(int id)
        {
            var result = await _usersDAL.GetUserById(id);
            var user = _mapper.Map<MemberDto>(result);
            return user;
        }

        public async Task<MemberDto> GetUserByName(string name)
        {
            var result = await _usersDAL.GetUserByUsername(name);
            var user = _mapper.Map<MemberDto>(result);
            return user;
        }

        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            var result = await _usersDAL.GetUsers();
            var users = _mapper.Map<IEnumerable<MemberDto>>(result);
            return users;
        }
    }
}
