using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration.Models;
public class ItemTransactionFinanceDTO : ItemTransactionFinance
    {
        public int ItemTransactionFinanceId { get; set; }
        public List<ItemTransactionFinanceLineCost>? itemTransactionFinanceLineCosts { get; set; }

    }

