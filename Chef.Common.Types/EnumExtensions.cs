using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Chef.Common.Types;

public static class EnumExtensions
{
    public static string GetDisplayName(Enum value)
    {
        return
            value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
            ?? value.ToString();
    }

    public static object GetDisplayName(object genderType)
    {
        throw new NotImplementedException();
    }

    public static Value GetEnumValueFromDescription<Value>(string description)
    {
        Type enumType = typeof(Value);  
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        FieldInfo[] fields = enumType.GetFields();
        foreach (FieldInfo field in fields)
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                {
                    return (Value)field.GetValue(null);
                }
            }
            else if (field.Name.Equals(description, StringComparison.OrdinalIgnoreCase))
            {
                return (Value)field.GetValue(null);
            }
        }

        throw new ArgumentException($"No enum value with the description '{description}' found.");
    }
}
