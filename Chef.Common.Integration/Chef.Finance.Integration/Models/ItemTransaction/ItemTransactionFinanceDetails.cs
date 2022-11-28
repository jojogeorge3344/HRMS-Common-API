
namespace Chef.Finance.Integration.Models;
public  class ItemTransactionFinanceDetails
    {
        public int ItemTransactionFinanceId { get; set; }
        public int ReferenceDocumentId { get; set; } //headerid
        public int businesspartnerid { get; set; }
        public int documenttype { get; set; }
        public string documentnumber { get; set; }
        public DateTime documentdate { get; set; }
        public DateTime transactiondate { get; set; }
        public int journalbookid { get; set; }
        public string journalbookcode { get; set; }
        public string transactioncurrencycode { get; set; }
        public decimal exchangerate { get; set; }
       // public DateTime exchangedate { get; set; }
        public string narration { get; set; }
        public decimal debitamountinbasecurrency { get; set; }
        public decimal creditamountinbasecurrency { get; set; }
        public int branchid { get; set; }
        public int approvestatus { get; set; }
      //  public int generalledgerid { get; set; }
        public int ledgeraccountid { get; set; }
        public string ledgeraccountcode { get; set; }
        public string ledgeraccountname { get; set; }
        public decimal debitamount { get; set; }
        public decimal creditamount { get; set; }
        public string businesspartnercode { get; set; }
        public int ReferenceDocumentdetailid { get; set; } // detailid
     //   public bool iscontrolaccount { get; set; }
        public int financialyearid { get; set; }
    }

