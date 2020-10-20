using Chef.Common.Models;

namespace Chef.Common.Dtos
{
    public class ItemSegmentEntryDto : ItemSegment
    {
        public bool AutoGenerateCode { get; set; } = true;
        public string Host { get; set; }
    }
}
