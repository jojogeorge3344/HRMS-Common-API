using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chef.Finance.Integration.Models;
public  class IntegrationDetalDimensionViewModel
    {
        public int Id { get; set; }
        public int integrationheaderid { get; set; }
        public int ledgeraccountid { get; set; }
        public string ledgeraccountcode { get; set; }
        public string ledgeraccountname { get; set; }
        public decimal debitamount { get; set; }
        public decimal debitamountinbasecurrency { get; set; }
        public decimal creditamount { get; set; }
        public decimal creditamountinbasecurrency { get; set; }

        public List<DetailDimension> detailDimensions { get; set; }

       
    }

    public class DetailDimension
    {
        public int Id { get; set; }

        public int integrationdetailid { get; set; }

        public string branchName { get; set; }

        public int branchId { get; set; }

        public string dimensionTypeName { get; set; }

        public int dimensionTypeId { get; set; }

        public string dimensionCode { get; set; }

        public int dimensionDetailId { get; set; }

        public string dimensionDetailName { get; set; }

        public decimal allocatedAmount { get; set; }
    }

