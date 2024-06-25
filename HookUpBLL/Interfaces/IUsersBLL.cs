using Entities.Dto;
using Entities.Helpers;

namespace HookUpBLL.Interfaces
{
    public interface IUsersBLL
    {
        Task<PagedList<MemberDto>> GetUsers(UserParams userParams);
        Task<MemberDto> GetUserById(int id);
        Task<MemberDto> GetUserByName(string name);
        Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto, string username);
        Task<MemberDto> GetMemberAsync(string username, bool isCurrentUser);
    }
}
