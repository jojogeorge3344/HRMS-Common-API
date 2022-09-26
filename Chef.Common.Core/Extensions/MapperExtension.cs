using AutoMapper;

namespace Chef.Common.Core
{
    public static class MapperExtension
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            System.Type destinationType = typeof(TDest);

            foreach (System.Reflection.PropertyInfo property in destinationType.GetProperties())
            {
                expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }
    }
}
