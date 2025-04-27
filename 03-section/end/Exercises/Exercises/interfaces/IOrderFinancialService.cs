
namespace Exercises.interfaces
{
    public interface IOrderFinancialService
    {
        public decimal CalculateOrderTotal(Order order);

        public decimal CalculateDiscount(Order order);

        public decimal CalculateTax(decimal amount);
    }
}
