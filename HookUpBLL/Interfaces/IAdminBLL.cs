﻿using Entities.Dto;

namespace HookUpBLL.Interfaces
{
    public interface IAdminBLL
    {
        public Task<IEnumerable<UserWithRolesDto>> GetUsersWithRoles();
        public Task<IList<string>> EditUserRoles(string username, string roles);
    }
}
