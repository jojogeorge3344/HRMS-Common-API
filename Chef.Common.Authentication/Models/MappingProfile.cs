namespace Chef.Common.Authentication.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.UserId, Opt => Opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.DefaultBranchId));

        //Email and username will be same.
        CreateMap<RegisterDto, ApplicationUser>();
        CreateMap<NewUserDto, ApplicationUser>();
    }
}
