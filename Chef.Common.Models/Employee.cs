using Chef.Common.Core;
using Chef.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Employee : Model
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Employee code should be maximum length of 10 characters")]
        [RegularExpression("^(?!0*$)[A-Za-z0-9\\-\\/]+$", ErrorMessage = "Employee code should be alphanumeric only")]
        public string EmployeeCode { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "First name should be maximum length of 50 characters")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "First name should be alphabet only")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Middle name should be maximum length of 50 characters")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "Middle name should be alphabet only")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name should be maximum length of 50 characters")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "Last name should be alphabet only")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Display name should be maximum length of 50 characters")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "Display name should be alphabet only")]
        public string DisplayName { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Phone number should be minimum length of 10 characters")]
        [MaxLength(18, ErrorMessage = "Phone number should be maximum length of 18 characters")]
        [RegularExpression("^([+]?[0-9- ]{1,18})+$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public string Department { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        public string Designation { get; set; }

        public int DesignationId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Address should be maximum length of 100 characters")]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
