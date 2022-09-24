namespace Chef.Common.Models
{
    public class RoleModuleView
    {
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }        
        public int ModuleId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int IsModuleActive { get; set; }
    }
}
