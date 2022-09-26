using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Login : Model
    {
        [EmailAddress]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}