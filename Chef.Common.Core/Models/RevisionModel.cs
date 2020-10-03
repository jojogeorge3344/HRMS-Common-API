using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class RevisionModel : Model
    {
        [Required]
        [Unique(true), Composite(Index = 1)]
        [Field(Order = 2)]
        public string Revision { get; set; }
        [Required]
        [Field(Order = 3)]
        public bool IsCurrentRevision { get; set; }
    }
}
