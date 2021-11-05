using Chef.Common.Core;
namespace Chef.Common.Models
{
    public class ProductionLine : Model
    {
        public int PlantId { get; set; }

        public string PlantName { get; set; }

        public string ProductionLineCode { get; set; }

        public string ProductionLineName { get; set; }

        public string Remarks { get; set; }

        public bool IsAssigned { get; set; }
    }
}
