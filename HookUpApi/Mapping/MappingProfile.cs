using AutoMapper;
using Entities.Models;
using HookUpDAL.Entities;

namespace HookUpApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, AppUser>().ReverseMap();
        }
    }
}
