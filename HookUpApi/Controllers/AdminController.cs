using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IAdminBLL _adminBLL;

        public AdminController(IAdminBLL adminBLL)
        {
            _adminBLL = adminBLL;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _adminBLL.GetUsersWithRoles();

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<IActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select atleast one role");
            var result = await _adminBLL.EditUserRoles(username, roles);
            return Ok(result);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public IActionResult GetPhotoForModeration()
        {
            return Ok("Admin or Moderator can see this");
        }
    }
}
