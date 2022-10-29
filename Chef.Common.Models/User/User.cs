using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class User : Model
    {
        [Unique(true)]
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeZone { get; set; }
        //TODO: Move this to Identity
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int DefaultBranchId { get; set; }
    }
}