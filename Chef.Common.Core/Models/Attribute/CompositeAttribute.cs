using System;

namespace Chef.Common.Core;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CompositeAttribute : Attribute
{
    /// <summary>
    /// Provides Grouping by the GroupNumber
    /// </summary>
    public int GroupNumber { get; set; } = 1;

    /// <summary>
    /// Index inside each group
    /// </summary>
    public int Index { get; set; }

    public CompositeAttribute() { }

    // public override object TypeId { get { return this; } }
}
