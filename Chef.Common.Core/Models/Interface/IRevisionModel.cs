using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public interface IRevisionModel : IModel
    { 
        public string Revision { get; set; } 
        public bool IsCurrentRevision { get; set; }
    }
}
