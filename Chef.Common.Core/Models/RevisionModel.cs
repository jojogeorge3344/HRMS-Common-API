using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class RevisionModel : Model
    {
        [Required]
        [Unique(true), Composite(Index = 1)]
        public string Revision { get; set; }
        [Required]
        public bool IsCurrentRevision { get; set; }
    }
}
