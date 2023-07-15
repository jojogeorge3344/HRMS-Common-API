using Chef.Common.Core;

namespace Chef.Trading.Models;

public interface ICurrencyModel : IModel
{
    public string TxnCurrencyCode { get; set; }
    public string TxnCurrencySymbol { get; set; }
}
