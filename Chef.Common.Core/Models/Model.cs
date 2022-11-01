using System;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Core
{
    public abstract class Model : IModel
    {
        [Write(false)]
        [Key]
        [Field(Order = 1)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsArchived { get; set; } = false;
    }

    public abstract class TransactionModel : Model
    {
        public int BranchId { get; set; }

        public int FinancialYearId { get; set; }

        ///TODO: CODE REVIEW: Why we need a TEMP Date. What is the business Reason?
        [Write(false)]
        [Skip(true)]
        private DateTime TempDate;

        public DateTime TransactionDate
        {
            get => TempDate.Date;

            set => TempDate = value;
        }
    }
}