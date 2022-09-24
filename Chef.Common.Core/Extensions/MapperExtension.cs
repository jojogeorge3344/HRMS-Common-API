using AutoMapper;

namespace Chef.Common.Core
{
    public static class MapperExtension
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            var destinationType = typeof(TDest);

            foreach (var property in destinationType.GetProperties())
            {
                expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }
    }
}
