
namespace Chef.Finance.Integration.Models;
public  class ItemTransactionFinanceDetailsDimension
    {
        public int integrationdetailid { get; set; }
        public string BranchName { get; set; }
        public int DimensionTypeId { get; set; }
        public string DimensionTypeName { get; set; }
        public int DimensionDetailId { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionDetailName { get; set; }
        public int BranchId { get; set; }
        public int FinancialYearId { get; set; }
        public DateTime TransactionDate { get; set; }
    }

