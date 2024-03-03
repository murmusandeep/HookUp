using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersBLL _usersBLL;

        public UsersController(IUsersBLL usersBLL)
        {
            _usersBLL = usersBLL;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersBLL.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersBLL.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _usersBLL.GetUserByName(username);
            return Ok(user);
        }
    }
}
