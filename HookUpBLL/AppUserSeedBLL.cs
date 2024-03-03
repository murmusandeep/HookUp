using AutoMapper;
using HookUpBLL.Interfaces;
using HookUpDAL.Entities;
using HookUpDAL.Interfaces;
using System.Text.Json;

namespace HookUpBLL
{
    public class AppUserSeedBLL : IAppUserSeedBLL
    {
        private readonly IAppUserSeedDAL _appUserSeedDAL;
        private readonly IMapper _mapper;

        public AppUserSeedBLL(IAppUserSeedDAL appUserSeedDAL, IMapper mapper)
        {
            _appUserSeedDAL = appUserSeedDAL;
            _mapper = mapper;
        }
        public void SaveSeedData(dynamic da)
        {

            var users = JsonSerializer.Deserialize<List<AppUser>>(da);

            _appUserSeedDAL.SaveSeedData(users);
        }
    }
}
