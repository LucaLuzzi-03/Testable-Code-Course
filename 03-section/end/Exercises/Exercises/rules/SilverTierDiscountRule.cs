
using Exercises.enums;
using Exercises.interfaces;

namespace Exercises.rules
{
    public class SilverTierDiscountRule : IDiscountOrderRule
    {
        public decimal CalculateDiscount(Order order) => order.TotalAmount * 0.05m;

        public bool IsApplicable(Order order) => order.Customer.Tier == CustomerTier.Silver;
    }
}
