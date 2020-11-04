﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Chef.Common.Repositories
{
    public static class ExpressionHelper
    {
        public static MemberExpression GetMemberExpression(LambdaExpression expression)
               => GetMemberExpression(expression.Body);

        public static MemberExpression GetMemberExpression(Expression expression)
        {
            MemberExpression result;

            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
                result = memberExpression;
            else
            {
                var unary = expression as UnaryExpression;
                if (unary != null && unary.NodeType == ExpressionType.Convert && unary.Operand is MemberExpression)
                {
                    result = (MemberExpression)unary.Operand;
                }
                else
                {
                    throw new NotSupportedException($"'{expression.GetType().FullName}' is not supported for member expression");
                }
            }
            return result;
        }
    }
}
