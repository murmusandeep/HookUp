using Entities.Models;

namespace HookUpApi.Interfaces
{
    public interface ITokenHelper
    {
        string CreateToken(User user);
    }
}
