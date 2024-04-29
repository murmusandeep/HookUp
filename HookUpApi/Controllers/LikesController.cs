using Entities.Helpers;
using HookUpApi.Extensions;
using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly ILikesBLL _likesBLL;

        public LikesController(ILikesBLL likesBLL)
        {
            _likesBLL = likesBLL;
        }
        [HttpPost("{username}")]
        public async Task<IActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            if (await _likesBLL.AddLike(username, sourceUserId)) return Ok();
            return BadRequest("Failed to like user");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();

            var users = await _likesBLL.GetUserLikes(likesParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }
    }
}
