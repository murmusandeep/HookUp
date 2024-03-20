using Entities.Dto;
using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _usersBLL.GetUserByName(username);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(MemberUpdateDto member)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (await _usersBLL.UpdateUser(member, username)) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}
