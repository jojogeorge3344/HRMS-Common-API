using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class RevisionModel : Model
    {
        [Required]
        public string Revision { get; set; }
    }
}
