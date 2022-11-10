using System;
using AutoMapper;

namespace Chef.Common.Authentication.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>();

        //Email and username will be same.
        CreateMap<RegisterDto, ApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName ));
    }
}
