using HookUpBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HookUpApi.Controllers
{
    public class AppUserSeedController : BaseApiController
    {
        private readonly IAppUserSeedBLL _appUserSeedBLL;

        public AppUserSeedController(IAppUserSeedBLL appUserSeedBLL)
        {
            _appUserSeedBLL = appUserSeedBLL;
        }

        [HttpPost("seeddata")]
        public void SeedData([FromBody] dynamic data)
        {
            _appUserSeedBLL.SaveSeedData(data);
        }
    }
}
