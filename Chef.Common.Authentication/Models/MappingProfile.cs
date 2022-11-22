using Chef.Common.Models;

namespace Chef.Common.Authentication.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>();

        //Email and username will be same.
        CreateMap<RegisterDto, ApplicationUser>();

        CreateMap<Branch, UserBranchEditDto>();

        CreateMap<IdentityRole, RoleDto>();
    }
}
