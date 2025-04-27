
using Exercises.interfaces;

namespace Exercises
{
    public class OrderFinancialService : IOrderFinancialService
    {
        private readonly IEnumerable<IDiscountOrderRule> _discountOrderRules;

        public OrderFinancialService(IEnumerable<IDiscountOrderRule> discountOrderRules)
        {
            _discountOrderRules = discountOrderRules;
        }

        public decimal CalculateOrderTotal(Order order)
        {
            decimal subtotal = 0;
            foreach (var item in order.Items)
            {
                subtotal += item.Price * item.Quantity;
            }

            // Calculate discount
            var discount = CalculateDiscount(order);

            // Calculate tax
            decimal tax = CalculateTax(subtotal - discount);

            return subtotal - discount + tax;
        }

        public decimal CalculateDiscount(Order order)
        {
            decimal discount = 0;

            // Customer tier discount
            foreach (var rule in _discountOrderRules)
            {
                if (rule.IsApplicable(order))
                {
                    discount += rule.CalculateDiscount(order);
                }
            }

            return discount;
        }

        public decimal CalculateTax(decimal amount) => amount * 0.07m;
    }
}
