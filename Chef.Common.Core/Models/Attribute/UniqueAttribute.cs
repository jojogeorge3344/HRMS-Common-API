﻿using System;

namespace Chef.Common.Core;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class UniqueAttribute : Attribute
{
    public bool IsUnique { get; set; }

    public UniqueAttribute(bool isUnique)
    {
        IsUnique = isUnique;
    }

    public override object TypeId => this;
}