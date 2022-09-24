using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Login :Model
    {
        [EmailAddress]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}