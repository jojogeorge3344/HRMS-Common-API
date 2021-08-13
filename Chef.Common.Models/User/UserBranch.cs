using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
   public class UserBranch: Model
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [ForeignKey("Branches")]
        public int BranchId { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string BranchName { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string BranchCode { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string AddressLine1 { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string AddressLine2 { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string CityName { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string StateName { get; set; }
        //[Write(false)]
        //[Skip(true)]
        //public string CountryName { get; set; }
    }
}
