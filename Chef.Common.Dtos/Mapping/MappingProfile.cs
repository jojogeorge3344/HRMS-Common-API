using AutoMapper;

namespace Chef.Common.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Chef.Common.Dtos.ItemSegmentEntryDto, Chef.Common.Models.ItemSegment>();

            CreateMap<Chef.Common.Dtos.ItemFamilyEntryDto, Chef.Common.Models.ItemFamily>();

            CreateMap<Chef.Common.Dtos.ItemClassEntryDto, Chef.Common.Models.ItemClass>();

            CreateMap<Chef.Common.Dtos.ItemCommodityEntryDto, Chef.Common.Models.ItemCommodity>();

            CreateMap<Chef.Common.Dtos.ItemEntryDto, Chef.Common.Models.Item>();

        }
    }
}
