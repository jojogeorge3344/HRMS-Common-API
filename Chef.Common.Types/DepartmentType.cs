using System.ComponentModel;

namespace Chef.Common.Types
{
    /// <summary>
    /// Holds Department Type
    /// </summary>
    public enum DepartmentType
    {
        [Description("Engineering")]
        Engineering = 1,

        [Description("Human Resource")]
        HumanResource,

        [Description("Human Resource")]
        Marketing,
    }
}
