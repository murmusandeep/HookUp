using Microsoft.AspNetCore.Identity;

namespace HookUpDAL.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
