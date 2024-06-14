using AutoMapper;
using Entities.Dto;
using Entities.Models;
using HookUpDAL.Entities;
using HookUpDAL.Extensions;
using Photo = HookUpDAL.Entities.Photo;


namespace HookUpApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())).ReverseMap();
            CreateMap<AppUser, User>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)))
                .ReverseMap();
            CreateMap<AppRole, Role>();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Photo, Image>().ReverseMap();
            CreateMap<MemberUpdateDto, AppUser>().ReverseMap();
            CreateMap<RegisterDto, AppUser>().ReverseMap();
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<AppUserWithRoles, UserWithRolesDto>().ReverseMap();
        }
    }
}
