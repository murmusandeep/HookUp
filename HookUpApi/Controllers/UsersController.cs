using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBLL _usersBLL;

        public UsersController(IUsersBLL usersBLL)
        {
            _usersBLL = usersBLL;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersBLL.GetUsers();
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersBLL.GetUserById(id);
            return Ok(user);
        }
    }
}
