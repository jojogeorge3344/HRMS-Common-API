using System;

namespace Chef.Common.Core
{
    public class UniqueAttribute : Attribute
    {
        public bool IsUnique { get; set; }

        public UniqueAttribute(bool isUnique)
        {
            this.IsUnique = isUnique;
        }
    }
}