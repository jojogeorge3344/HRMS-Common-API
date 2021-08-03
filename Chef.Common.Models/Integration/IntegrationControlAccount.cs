using Chef.Common.Core;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
								public int ChartOfAccountId { get; set; }

								[Required]
								public string ChartOfAccountCode { get; set; }

								[Required]
								public string ChartOfAccountName { get; set; }
				}
} 
