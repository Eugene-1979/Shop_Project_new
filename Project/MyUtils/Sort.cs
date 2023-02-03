using System.Linq.Expressions;

namespace Shop_Project.MyUtils
    {
    public class Sort
        {
        public static IQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
            {
            if(!string.IsNullOrEmpty(propertyName))
                try
                    {
                    ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
                    MemberExpression property = Expression.PropertyOrField(param, propertyName);
                    LambdaExpression sort = Expression.Lambda(property, param);

                    MethodCallExpression call = Expression.Call(
                        typeof(Queryable),
                        (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                        new[] { typeof(T), property.Type },
                        source.Expression,
                        Expression.Quote(sort));
                    return (IQueryable<T>)source.Provider.CreateQuery<T>(call);
                    }
                catch
                    {
                    return null;
                    }
            return null;
            }
        }
    }
