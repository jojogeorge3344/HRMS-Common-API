using System;
using System.ComponentModel.DataAnnotations;
using static Chef.Common.Core.TransactionModel;

namespace Chef.Common.Core
{
    public abstract class Model : IModel
    {
        [Write(false)]
        [Key]
        [Field(Order = 1)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; } 

        public string ModifiedBy { get; set; } 

        public bool IsArchived { get; set; } = false;
       
    }
    
    public abstract class TransactionModel : Model
    {
       public int BranchId { get; set; }

        public int FinancialYearId { get; set; }
        [Write(false)]
        [Skip(true)]
        private DateTime tempDate;

    
        public DateTime TransactionDate
        {
            get
            {
                return this.tempDate.Date;
            }



            set
            {
                this.tempDate = value;
            }
        }
    }
}