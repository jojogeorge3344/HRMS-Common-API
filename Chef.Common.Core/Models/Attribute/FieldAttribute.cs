using System;

namespace Chef.Common.Core;

public class FieldAttribute : Attribute
{
    public int Order { get; set; }

    public FieldAttribute() { }
}
