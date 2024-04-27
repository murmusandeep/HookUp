﻿using HookUpApi.Extensions;
using HookUpDAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HookUpApi.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUsersDAL>();

            var user = await repo.GetUserById(userId);

            user.LastActive = DateTime.UtcNow;

            await repo.SaveAllAsync();
        }
    }
}
