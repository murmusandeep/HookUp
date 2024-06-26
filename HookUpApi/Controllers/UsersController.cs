﻿using Entities.Dto;
using Entities.Helpers;
using HookUpApi.Extensions;
using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUsersBLL _usersBLL;
        private readonly IPhotoBLL _photoBLL;

        public UsersController(IUsersBLL usersBLL, IPhotoBLL photoBLL)
        {
            _usersBLL = usersBLL;
            _photoBLL = photoBLL;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            var currentUser = await _usersBLL.GetUserByName(User.GetUsername());

            userParams.CurrentUsername = currentUser.UserName;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userParams.Gender == "male" ? "female" : "male";
            }

            var users = await _usersBLL.GetUsers(userParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var currentUsername = User.GetUsername();
            var result = await _usersBLL.GetMemberAsync(username, isCurrentUser: currentUsername == username);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(MemberUpdateDto member)
        {
            if (await _usersBLL.UpdateUser(member, User.GetUsername())) return NoContent();
            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            var result = await _photoBLL.AddPhotoAsync(User.GetUsername(), file);
            return CreatedAtAction(nameof(GetUserByUsername), new { username = User.GetUsername() }, result);
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            if (await _photoBLL.DeletePhotoAsync(User.GetUsername(), photoId)) return NoContent();
            return BadRequest("Problem deleting photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<IActionResult> SetMainPhoto(int photoId)
        {
            if (await _photoBLL.SetMainPhoto(User.GetUsername(), photoId)) return NoContent();
            return BadRequest("Problem setting main photo");
        }
    }
}
