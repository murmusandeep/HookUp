using AutoMapper;
using Entities.Dto;
using Entities.Models;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<User> GetUser(string username)
        {
            var result = await _accountDAL.GetUser(username);

            var user = _mapper.Map<User>(result);

            return user;
        }

        public async Task<User> Register(RegisterDto register)
        {
            var appUser = _mapper.Map<AppUser>(register);

            using var hmac = new HMACSHA512();

            appUser.UserName = register.UserName.ToLower();
            appUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password));
            appUser.PasswordSalt = hmac.Key;

            await _accountDAL.Register(appUser);

            var user = _mapper.Map<User>(appUser);

            return user;
        }

        public Task<bool> UserExists(string username)
        {
            return _accountDAL.UserExists(username);
        }
    }
}
