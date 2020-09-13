using System;

namespace Chef.Common.Core
{
    public class WriteAttribute : Attribute
    {
        public bool Write { get; set; }

        public WriteAttribute(bool write)
        {
            this.Write = write;
        }
    }
}