using Entities.Dto;

namespace HookUpBLL.Interfaces
{
    public interface IUsersBLL
    {
        Task<IEnumerable<MemberDto>> GetUsers();
        Task<MemberDto> GetUserById(int id);
        Task<MemberDto> GetUserByName(string name);
    }
}
