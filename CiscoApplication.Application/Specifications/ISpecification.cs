using System.Linq.Expressions;

namespace CiscoApplication.Application.Specifications
{
    public interface ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderByAsc { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
