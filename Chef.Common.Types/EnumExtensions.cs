using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Chef.Common.Types
{
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
    }
}
