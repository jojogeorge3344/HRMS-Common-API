namespace Chef.Common.Models
{
    public class UserBranchView
    {
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int IsBranchActive { get; set; }
    }
}
