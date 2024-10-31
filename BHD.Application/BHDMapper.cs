using AutoMapper;
using BHD.Application.Dtos.Phone;
using BHD.Application.Dtos.User;
using BHD.Domain.Models;
using BHD.Models.Models;

namespace BHD.Application
{
    public class BHDMapper : Profile
    {
        public BHDMapper()
        {
            CreateMap<User, CreatedUserDto>()
                .ForMember(x => x.Created, cfg => cfg.MapFrom(y => y.CreatedAt))
                .ForMember(x => x.Modified, cfg => cfg.MapFrom(y => y.ModifiedAt))
                .ForMember(x => x.Token, cfg => cfg.MapFrom(x => x.Token))
                .ForMember(x => x.Last_Login, cfg => cfg.MapFrom(x => x.LastLogin))
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(x => x.Phones, cfg => cfg.Ignore())
                .ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();

        }
    }
}
