using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlKata;

namespace Chef.Finance.Integration.Models;

public class SalesOrderReceiptDto
{

    [Required]
    [StringLength(32)]
    public string ReceiptNumber { get; set; }

    [Required]
    public DateTime ReceiptDate { get; set; }

    [Required]
    public int CompanyId { get; set; }

    [Required]
    //[ForeignKey("Common.Branch")]
    public int BranchId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public string CustomerCode { get; set; }

    public string CustomerName { get; set; }



    public int ReceivedById { get; set; }

    public string ReceivedByName { get; set; }


  
    public string TransactionCurrencyCode { get; set; }
    public string baseCurrencyCode { get; set; }
    public DateTime transactionCurrencyDate { get; set; }

    public int ExchangeRateId { get; set; }

    public decimal ExchangeRate { get; set; }

    public DateTime ExchangeDate { get; set; }

    public string ReceiptVoucherNumber { get; set; }

    [StringLength(250)]
    public string Comments { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal TotalAmountInBaseCurrency { get; set; }

    [Required]
    public string TransactionReference { get; set; }
    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string CashAccountNumber { get; set; }


}

