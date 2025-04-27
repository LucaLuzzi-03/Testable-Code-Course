
namespace Exercises.interfaces
{
    public interface IDiscountOrderRule
    {
        public bool IsApplicable(Order order);

        public decimal CalculateDiscount(Order order);
    }
}
