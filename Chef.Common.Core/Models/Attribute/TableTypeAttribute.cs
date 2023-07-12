using System;

namespace Chef.Common.Core;

public class TableTypeAttribute : Attribute
{
    public TableTypeAttribute(string type)
    {
        Type = type;
    }

    public string Type { get; set; }
}