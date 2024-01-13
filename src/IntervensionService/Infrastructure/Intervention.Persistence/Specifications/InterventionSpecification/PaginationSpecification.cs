using System.Linq.Expressions;
using System.Reflection;
using Intervention.Application.Contracts.Persistence.Specifications;
using Intervention.Application.Contracts.Specs;

namespace Intervention.Persistence.Specifications.InterventionSpecification;

public class PaginationSpecification<T> : ISpecification<T>
{
    public PaginationSpecification(SpecParams specParams)
    {
        Criteria = BuildExpression(specParams.Search!);
        IsPagingEnabled = true;
        Take = specParams.PageSize;
        Skip = specParams.PageSize * specParams.PageIndex;
        if (specParams.SortOrder == "desc")
            OrderByDescending = GetOrderByExpression(specParams.SortBy, specParams.SortOrder);
    }

    public Expression<Func<T, bool>> BuildExpression(List<SearchParam> searchParams)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        Expression body = Expression.Constant(true);

        foreach (var searchParam in searchParams)
        {
            var property = Expression.Property(parameter, searchParam.Property);
            var filterValue = Expression.Constant(searchParam.Value);
            var equalityExpression = Expression.Equal(property, filterValue);
            body = Expression.AndAlso(body, equalityExpression);
        }

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private Expression<Func<T, object>> GetOrderByExpression(string sortBy, string sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy) || string.IsNullOrWhiteSpace(sortOrder)) return null;
        var parameter = Expression.Parameter(typeof(T));
        var property = typeof(T).GetProperty(sortBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null) return null;

        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var lambda =
            Expression.Lambda<Func<T, object>>(Expression.Convert(propertyAccess, typeof(object)),
                parameter);

        return sortOrder.ToLower() == "desc" ? Reverse(lambda) : lambda;
    }

    public static Expression<Func<T, object>> Reverse<T>(Expression<Func<T, object>> expression)
    {
        var body = Expression.Convert(expression.Body, typeof(object));
        return Expression.Lambda<Func<T, object>>(body, expression.Parameters);
    }

    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; }
    public List<string> IncludeStrings { get; }
    public Expression<Func<T, object>> OrderBy { get; }
    public Expression<Func<T, object>> OrderByDescending { get; }
    public Expression<Func<T, object>> GroupBy { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsPagingEnabled { get; }
}