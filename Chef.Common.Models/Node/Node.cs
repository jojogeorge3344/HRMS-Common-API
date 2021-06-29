using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Node: Model
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int ParentId { get; set; }

        [Write(false)]
        [Skip(true)]
        public string ModuleName { get; set; }
        [Write(false)]
        [Skip(true)]
        public int TotalDocumentCount { get; set; }
        [Write(false)]
        [Skip(true)]
        public string SubModuleName { get; set; }

    }
}
