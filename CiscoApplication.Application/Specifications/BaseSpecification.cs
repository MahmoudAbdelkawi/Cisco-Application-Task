using System.Linq.Expressions;

namespace CiscoApplication.Application.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; set; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsc { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criterias.Add(criteria);
        }
        public void AddInclude(Expression<Func<T, object>> Include)
        {
            Includes.Add(Include);
        }
        public void AddOrderByAsc(Expression<Func<T, object>> orderAsc)
        {
            OrderByAsc = orderAsc;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderDesc)
        {
            OrderByDesc = orderDesc;
        }
        public void ApplyPagination(int _skip, int _take)
        {
            IsPaginationEnabled = true;
            Skip = _skip;
            Take = _take;
        }
    }
}
