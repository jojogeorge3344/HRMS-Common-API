using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class AttributeModel : Model
    {
        [Required]
        [Unique(true), Composite(Index =1)]
        public string AttributeName { set; get; }
        public string AttributeValue { set; get; }
    }
}
