using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Authentication : Model
    {
        /// <summary>
        /// Holds email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Holds password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Holds token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Holds employee id
        /// </summary>
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
    }
}