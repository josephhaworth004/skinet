using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> // BaseSpecification.cs will implement this interface
    {
        // Generic Methods
        // Criteria is where our "Where" method will be
        Expression<Func<T, bool>> Criteria { get; }    
        List<Expression<Func<T, object>>> Includes { get; } // object is the most generic return type
    }
}