using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class IntegrationAccountSummary : Model
{
    [Required]
    public int AccountId { get; set; }

    [Required]
    public string AccountCode { get; set; }

    [Required]
    public string AccountDesc { get; set; }

    [Required]
    public decimal DebitValue { get; set; }

    [Required]
    public decimal DebitValueInBaseCurrency { get; set; }

    [Required]
    public decimal CreditValue { get; set; }

    [Required]
    public decimal CreditValueInBaseCurrency { get; set; }

    [Required]
    public bool IsDebit { get; set; }
}
