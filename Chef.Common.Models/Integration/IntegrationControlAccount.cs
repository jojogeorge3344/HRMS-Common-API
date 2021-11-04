using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;
using Newtonsoft.Json;

namespace Chef.Common.Models
{
    public class IntegrationControlAccount : Model
	{
		[Required]
		[ForeignKey("FinanceIntegrationConfiguration")]
		public int FinanceIntegrationConfigurationId { get; set; }

		[Required]
		public int ControlAccountType { get; set; }

		[Required]
		[ForeignKey("finance.ChartOfAccount")]
		public int ChartOfAccountId { get; set; }

		[Required]
		public string ChartOfAccountCode { get; set; }

		[Required]
		public string ChartOfAccountName { get; set; }

		[Required]
		[ForeignKey("ItemControlAccountMaster")]
		public int ItemControlAccountDescId { get; set; }

		[Required]
		public string ItemControlAccountDescCode { get; set; }

		[Required]
		public string ItemControlAccountDesc { get; set; }
	}
}
