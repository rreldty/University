using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace University.Dto.Base
{
    public static class Extension
    {
        public enum SortOrder
        {
            ASC,
            DESC,
        }

        public static IOrderedEnumerable<T> Sort<T>(IEnumerable<T> source, Dictionary<string, SortOrder> sortOptions)
        {
            IOrderedEnumerable<T> result = null;

            try
            {
                foreach (KeyValuePair<string, SortOrder> entry in sortOptions)
                {
                    if (result != null)
                    {
                        if (entry.Value == SortOrder.ASC)
                            result = result.ApplyOrder<T>(entry.Key, "ThenBy");
                        else
                            result = result.ApplyOrder<T>(entry.Key, "ThenByDescending");
                    }
                    else
                    {
                        if (entry.Value == SortOrder.ASC)
                            result = source.ApplyOrder<T>(entry.Key, "OrderBy");
                        else
                            result = source.ApplyOrder<T>(entry.Key, "OrderByDescending");
                    }
                }

                return result;
            }
            catch (ArgumentException ex)
            {
                string strMsg = ex.Message;
                return result;
            }
        }

        private static IOrderedEnumerable<T> ApplyOrder<T>(this IEnumerable<T> source, string property, string methodName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression expr = param;
            foreach (string prop in property.Split('.'))
            {
                // use reflection (not ComponentModel) to mirror LINQ
                expr = Expression.PropertyOrField(expr, prop);
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), expr.Type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, param);

            MethodInfo mi = typeof(Enumerable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), expr.Type);
            return (IOrderedEnumerable<T>)mi.Invoke(null, new object[] { source, lambda.Compile() });
        }
    }
}
