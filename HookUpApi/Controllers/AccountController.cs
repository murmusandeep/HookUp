using Entities.Dto;
using HookUpApi.Interfaces;
using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountBLL _accountBLL;
        private readonly ITokenHelper _tokenHelper;

        public AccountController(IAccountBLL accountBLL, ITokenHelper tokenHelper)
        {
            _accountBLL = accountBLL;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (await _accountBLL.UserExists(register.UserName)) return BadRequest("Username is Taken");

            var user = await _accountBLL.Register(register);

            var userDto = new UserDto
            {
                Username = user.UserName,
                Token = _tokenHelper.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _accountBLL.GetUser(loginDto.Username, loginDto.Password);

            if (user == null) return Unauthorized("Invalid Username");

            var userDto = new UserDto
            {
                Username = user.UserName,
                Token = _tokenHelper.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };

            return Ok(userDto);
        }
    }
}
