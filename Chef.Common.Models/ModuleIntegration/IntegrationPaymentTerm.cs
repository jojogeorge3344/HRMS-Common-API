using Chef.Common.Core;
using System.Collections.Generic;

namespace Chef.Common.Models
{
    public class IntegrationPaymentTerm : Model
    {
        public decimal Amount { get; set; }

        public decimal AmountBaseCurrency { get; set; }

        public decimal AdvanceAmount { get; set; } = 0;

        public decimal AdvanceAmountInBaseCurrecy { get; set; } = 0;

        public int NumberOfInstalments { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<IntegrationPaymentTermInstallment> Instalments { get; set; }
    }
}