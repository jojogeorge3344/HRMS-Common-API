using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Chef.Common.Repositories
{
    public static class ExpressionHelper
    {
        public static MemberExpression GetMemberExpression(LambdaExpression expression)
               => GetMemberExpression(expression.Body);
        public static IEnumerable<MemberExpression> GetMemberExpressions(NewExpression expression)
        {
            return expression.Arguments.Select(item => GetMemberExpression(item));
        }
        public static IEnumerable<string> GetMemberNames(NewExpression expression)
        {
            return expression.Members.Select(item => item.Name);
        }

        public static MemberExpression GetMemberExpression(Expression expression)
        {
            //MemberExpression result;
            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
                return memberExpression;
            var unary = expression as UnaryExpression;
            if (unary != null && unary.NodeType == ExpressionType.Convert && unary.Operand is MemberExpression)
            {
                return (MemberExpression)unary.Operand;
            }
            else
            {
                throw new NotSupportedException($"'{expression.GetType().FullName}' is not supported for member expression");
            }
        }

        public static string GetPropertyName(this LambdaExpression propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));
            return ExpressionHelper.GetMemberExpression(propertyExpression).GetPropertyName();
        }
        public static string GetPropertyName(this MemberExpression memberExpression)
        {
            if (memberExpression == null) throw new ArgumentNullException(nameof(memberExpression));
            return memberExpression.Member.Name.ToLower();
        }
        static string TableNameWOSchema<T>() => typeof(T).Name.ToLower();
         
        public static string GetFieldName<T>(this LambdaExpression propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));
            return string.Format("{0}.{1}", TableNameWOSchema<T>(), ExpressionHelper.GetMemberExpression(propertyExpression).GetPropertyName());
        }
    }
}
