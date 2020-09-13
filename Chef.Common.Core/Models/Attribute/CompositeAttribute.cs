using System;

namespace Chef.Common.Core
{
    public class CompositeAttribute : Attribute
    {
        public int Index { get; set; }
        public CompositeAttribute() { }
    }
}
