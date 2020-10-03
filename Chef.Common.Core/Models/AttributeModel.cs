using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class AttributeModel : Model
    {
        [Required]
        [Unique(true), Composite(Index =1)]
        [Field(Order = 2)]
        public string AttributeName { set; get; }
        [Field(Order = 3)]
        public string AttributeValue { set; get; }
    }
}
