using Chef.Common.Core;
namespace Chef.Finance.Integration.Models;

public class TradingIntegrationHeader : TransactionModel
    {
        public string documentnumber { get; set; }

        public DocumentType documentType { get; set; }
        public int journalbookid { get; set; }
        public string journalbookcode { get; set; }
        public string referencenumber { get; set; }
        public string branchcode { get; set; }
        public int transorginid { get; set; }
        public string transorgin { get; set; }
        public int transtypeid { get; set; }
        public string transtype { get; set; }
        public int companyid { get; set; }
        public string company { get; set; }
        public int? businesspartnerid { get; set; }
        public string? businesspartnercode { get; set; }
        public string? businesspartnername { get; set; }
        public int transtypeslno { get; set; }
        public int transid { get; set; }
        public string currency { get; set; }
        public string exchangerate { get; set; }
        public decimal totalamount { get; set; } //TODO change type in db
        public string? remark { get; set; }
        public ApproveStatus ApproveStatus { get; set; }
        public string ApproveStatusName { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime RejectedDate { get; set; }

    public DateTime ExchangeDate { get; set; }

    [Write(false)]
    [Skip(true)]
    [Ignore]

    public string Narration { get; set; }

    [Write(false)]
        [Skip(true)]
        [Ignore]
        public decimal DiscountAmount { get; set; }

        [Write(false)]
        [Skip(true)]
        [Ignore]
    public decimal TaxAmount { get; set; }

        [Write(false)]
        [Skip(true)]
    [Ignore]
    public decimal GrandToatal { get; set; }

        [Write(false)]
        [Skip(true)]
        [Ignore]
    public List<IntegrationDetails> integrationDetails { get; set; }
    }

