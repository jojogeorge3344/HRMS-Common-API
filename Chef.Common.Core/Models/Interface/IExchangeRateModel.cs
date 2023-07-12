using Chef.Common.Core;
using System;

namespace Chef.Trading.Models;

public interface IExchangeRateModel : IModel, ICurrencyModel
{
    public string BaseCurrencyCode { get; set; }
    public string BaseCurrencySymbol { get; set; }
    public float TxnCurrencyExchangeRate { get; set; }
    public DateTime TxnCurrencyExchangeDate { get; set; }
    public bool IsExchangeRateFixed { get; set; }
}
