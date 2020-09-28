using System;

namespace Chef.Common.Core
{
    public class CompositeAttribute : Attribute
    {
        public string Key { get; set; }
        public int Index { get; set; }
        public CompositeAttribute() { }
    }
}
