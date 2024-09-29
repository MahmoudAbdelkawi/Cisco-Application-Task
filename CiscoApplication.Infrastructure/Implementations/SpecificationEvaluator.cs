using CiscoApplication.Application.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CiscoApplication.Infrastructure.Implementations
{
    internal class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQueryWithSpec(IQueryable<TEntity> querystart, ISpecification<TEntity> spec)
        {
            var query = querystart;
            if (spec.Criterias.Any()) query = spec.Criterias.Aggregate(query, (currentQuery, Criteria) => currentQuery.Where(Criteria));
            query = spec.Includes.Aggregate(query, (currentQuery, Include) => currentQuery.Include(Include));
            if (spec.OrderByAsc is not null) query = query.OrderBy(spec.OrderByAsc);
            if (spec.OrderByDesc is not null) query = query.OrderByDescending(spec.OrderByDesc);
            if (spec.IsPaginationEnabled) query = query.Skip(spec.Skip).Take(spec.Take);
            return query;
        }
    }
}
