using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities.Dto;
using Entities.Exceptions;
using Entities.Helpers;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedList<MemberDto>> GetUsers(UserParams userParams)
        {
            var result = await _usersDAL.GetUsers();

            result = result.Where(u => u.UserName != userParams.CurrentUsername);
            result = result.Where(u => u.Gender == userParams.Gender);

            var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
            var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

            result = result.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            result = userParams.OrderBy switch
            {
                "created" => result.OrderByDescending(u => u.Created),
                _ => result.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(
                result.AsNoTracking().ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
                userParams.PageNumber,
                userParams.PageSize);
        }

        public async Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto, string username)
        {
            var user = await GetUserAndCheckIfItExists(username);
            _mapper.Map(memberUpdateDto, user);
            return await _usersDAL.SaveAllAsync();
        }

        private async Task<AppUser> GetUserAndCheckIfItExists(string username)
        {
            var user = await _usersDAL.GetUserByUsername(username);
            if (user is null)
                throw new UserNotFoundException(username);
            return user;
        }
    }
}
