namespace Chef.Common.Core
{
    public interface IUserToken
    {
        public string UserName { get; }
        public string CompanyCode { get; }
        public string CompanyName { get; }
        public string BranchCode { get; }
        public string BranchName { get; }
        public string BaseCurrencyCode { get; }
        public string BaseCurrencySymbol { get; }
    }
}