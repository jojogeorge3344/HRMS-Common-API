using System;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class Model
    {
        [Write(false)]
        [Key]
        [Field(Order = 1)]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public bool IsArchived { get; set; } = false;
    }
}