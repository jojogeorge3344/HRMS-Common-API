using System;
using Dapper.Contrib.Extensions;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace Chef.Common.Core
{
    public abstract class Model : IModel
    {
        [Write(false)]
        [Key]
        [Field(Order = 1)]
        [SqlKata.Ignore]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; } = HttpHelper.Username;

        public string ModifiedBy { get; set; }= HttpHelper.Username;

        public bool IsArchived { get; set; } = false;
    }

    public abstract class TransactionModel : Model
    {
        public int BranchId { get; set; }

        public int FinancialYearId { get; set; }

        ///TODO: CODE REVIEW: Why we need a TEMP Date. What is the business Reason?
        private DateTime TempDate;

        //public DateTime TransactionDate
        //{
        //    get => TempDate.Date;

        //    set => TempDate = value;
        //}
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}