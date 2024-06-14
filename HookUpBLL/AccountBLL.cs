using AutoMapper;
using Entities.Dto;
using Entities.Exceptions;
using Entities.Models;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;

namespace HookUpBLL
{
    public class AccountBLL : IAccountBLL
    {
        private readonly IAccountDAL _accountDAL;
        private readonly IMapper _mapper;

        public AccountBLL(IAccountDAL accountDAL, IMapper mapper)
        {
            _accountDAL = accountDAL;
            _mapper = mapper;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var appUser = await _accountDAL.GetUser(username);

            var result = await _accountDAL.CheckUserValid(appUser, password);

            if (!result)
                throw new UnAuthorizedException("UnAuthorized");

            var user = _mapper.Map<User>(appUser);

            return user;
        }

        public async Task<User> Register(RegisterDto register)
        {
            var appUser = _mapper.Map<AppUser>(register);

            appUser.UserName = register.UserName.ToLower();

            var result = await _accountDAL.Register(appUser, register.password);

            if (!result.Succeeded)
                throw new BadRequestException("Unable to Register");

            var roleResult = await _accountDAL.AddUserRole(appUser);

            if (!roleResult.Succeeded)
                throw new BadRequestException("Unable to Add Role");

            var user = _mapper.Map<User>(appUser);

            return user;
        }

        public Task<bool> UserExists(string username)
        {
            return _accountDAL.UserExists(username);
        }
    }
}
