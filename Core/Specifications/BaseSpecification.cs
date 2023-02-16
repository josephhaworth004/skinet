using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        } // This constructor is for setting all products

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        } // This constructor is for setting one product based on id arg sent

        public Expression<Func<T, bool>> Criteria {get; }

        public List<Expression<Func<T, object>>> Includes {get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);    
        }
    }
}