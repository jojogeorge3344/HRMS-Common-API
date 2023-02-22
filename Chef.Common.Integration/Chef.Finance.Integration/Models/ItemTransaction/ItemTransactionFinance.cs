using Chef.Common.Core;

namespace Chef.Finance.Integration.Models;
public class ItemTransactionFinance : Model
    {
        public int TransOrginId { get; set; }
        public string TransOrgin { get; set; }
        public int TransTypeId { get; set; }
        public string TransType { get; set; }
        public string Company { get; set; }
        public int BranchId { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchName { get; set; }
        public int? BpId { get; set; }
        public string? BpCode { get; set; }
        public string? BpName { get; set; }
        public int TrasnTypeSlNo { get; set; }
        public int TransId { get; set; }
        public string TrasnOrderNum { get; set; }
        public DateTime TrasnDate { get; set; }
        public string Currency { get; set; }
        public decimal ExRate { get; set; }
        public string? TransRemark { get; set; }
        public int ItemCategory { get; set; }
        public int ItemType { get; set; }
        public int ItemSegmentId { get; set; }        
        public int ItemFamilyId { get; set; }
        public int ItemClassId { get; set; }
        public int ItemCommodityId { get; set; }
        public int ItemId { get; set; }
        public int ItemLineNo { get; set; }
        public int ItemTransType { get; set; }
        public int LandingCostId { get; set; }
        public int TransUomId { get; set; }
        public decimal UnitRate { get; set; }
        public decimal TransQty { get; set; }
        public decimal TransAmount { get; set; }
        public decimal HmAmount { get; set; }
        public int Status { get; set; } //0-pending,1-docnumgenerated

        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }

        public int CostCenterId { get; set; }
        public string CostCenterCode { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string ReasonCode { get; set; }

        public int GroupId { get; set; }

    public DateTime? ExchangeDate { get; set; }
}

