using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class BankBranch : Model
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }

        [ForeignKey("Common.Bank")]
        public int BankId { get; set; }

        [Required]
        [StringLength(256)]
        public string BankName { get; set; }

        [StringLength(256)]
        public string AddressLine1 { get; set; }

        [StringLength(256)]
        public string AddressLine2 { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [StringLength(126)]
        public string CityName { get; set; }

        [ForeignKey("Common.State")]
        public int StateId { get; set; }

        [StringLength(126)]
        [Required]
        public string StateName { get; set; }

        [ForeignKey("Common.Country")]
        public int CountryId { get; set; }

        [StringLength(126)]
        [Required]
        public string CountryName { get; set; }

        [Required]
        [StringLength(8)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(11)]
        public string IFSC { get; set; }

        [StringLength(11)]
        public string SWIFTBIC { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(12)]
        public string Fax { get; set; }

        [StringLength(32)]
        public string Email { get; set; }

        [StringLength(64)]
        public string Website { get; set; }
    }
}