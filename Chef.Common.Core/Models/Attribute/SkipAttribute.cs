using System;

namespace Chef.Common.Core
{
    public class SkipAttribute : Attribute
    {
        public bool Skip { get; set; }

        public SkipAttribute(bool skip)
        {
            this.Skip = skip;
        }
    }
}
