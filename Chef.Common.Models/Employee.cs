using Chef.Common.Core;
using System;

namespace Chef.Common.Models
{
    public class Employee : Model
    {
        public string EmployeeCode { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public GenderType Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime DateOfJoining { get; set; }

        public string Department { get; set; }

        public int DepartmentId { get; set; }

        public string Designation { get; set; }

        public int DesignationId { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

    }
}
