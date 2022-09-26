using Chef.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class UserSignature : Model
    {
        [ForeignKey("Common.User")]
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string FileName { get; set; }

        public byte[] Signature { get; set; }

        [Skip(true)]
        [Write(false)]
        public string Sign { get; set; }
    }
}
