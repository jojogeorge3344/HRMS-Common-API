using Chef.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Finance.Integration.Models;
public class IntegrationDetailDimension: TransactionModel
    {
        [ForeignKey("IntegrationDetails")]
        public int Integrationdetailid { get; set; }
        public string BranchName { get; set; }
        public int DimensionTypeId { get; set; }
        public string DimensionTypeName { get; set; }
        public int DimensionDetailId { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionDetailName { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmountInBaseCurrency { get; set; }
        public decimal? CreditAmountInBaseCurrency { get; set; }

        public int HeaderId { get; set; }


    }

