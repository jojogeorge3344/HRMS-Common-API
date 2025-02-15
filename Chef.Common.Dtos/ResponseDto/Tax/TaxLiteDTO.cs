﻿namespace Chef.Common.Dtos
{
    public class TaxLiteDTO
    {
        public string TaxCode { get; set; }
        public string TaxDescription { get; set; }
        public int TaxJurisdictionId { get; set; }
        public int ItemSegmentId { get; set; }
        public int ItemClassId { get; set; }
        public int MainTax { get; set; }
        public int TaxPercentage { get; set; }
    }
}