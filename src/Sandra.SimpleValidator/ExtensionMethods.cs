using System;
using System.Linq.Expressions;

namespace Sandra.SimpleValidator
{
    public static class ExtensionMethods
    {
        public static string GetCorrectPropertyName<T>(this Expression<Func<T, dynamic>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return ((MemberExpression) expression.Body).Member.Name;
            }

            var op = ((UnaryExpression) expression.Body).Operand;

            return ((MemberExpression) op).Member.Name;
        }
    }
}