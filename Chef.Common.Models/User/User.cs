using Chef.Common.Core;

namespace Chef.Common.Models
{
   public class User:Model
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emailid { get; set; }
        public string TimeZone { get; set; }
        public string Password { get; set; }
        
    }
}
