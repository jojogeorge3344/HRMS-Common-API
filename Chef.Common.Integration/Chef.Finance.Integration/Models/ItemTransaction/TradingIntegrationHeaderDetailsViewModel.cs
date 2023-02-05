using Chef.Common.Core;

namespace Chef.Finance.Integration.Models;
public  class TradingIntegrationHeaderDetailsViewModel
    {
        public int TradingIntegrationHeaderId { get; set; }
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
        public int businesspartnerid { get; set; }
        public string businesspartnercode { get; set; }
        public string businesspartnername { get; set; }
        public int transtypeslno { get; set; }
        public int transid { get; set; }
        public string currency { get; set; }
        public decimal exchangerate { get; set; }
        public decimal totalamount { get; set; } //TODO change type in db
        public string remark { get; set; }
        public ApproveStatus ApproveStatus { get; set; }
        public string ApproveStatusName { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime RejectedDate { get; set; }

        public DateTime TransactionDate { get; set; }



        public int IntegrationDetailId { get; set; }
        public int integrationheaderid { get; set; }
        public int linenumber { get; set; }
        public int ledgeraccountid { get; set; }
        public string ledgeraccountcode { get; set; }
        public string ledgeraccountname { get; set; }
        public decimal debitamount { get; set; }
        public decimal debitamountinbasecurrency { get; set; }
        public decimal creditamount { get; set; }
        public decimal creditamountinbasecurrency { get; set; }
        public bool isdimensionallocation { get; set; }
        public string narration { get; set; }

        [Write(false)]
        [Skip(true)]
        public int ItemTransactionFinanceId { get; set; }

        [Write(false)]
        [Skip(true)]
        public int ItemTransactionFinanceLineCostId { get; set; }

        public int BranchId { get; set; }

        public int FinancialYearId { get; set; }
    }

