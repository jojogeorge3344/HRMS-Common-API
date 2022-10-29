﻿using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class User : Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emailid { get; set; }
        public string TimeZone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int DefaultBranchId { get; set; }
    }
}