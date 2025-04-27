
using Exercises.enums;
using Exercises.interfaces;

namespace Exercises.rules
{
    public class GoldTierDiscountRule : IDiscountOrderRule
    {
        public decimal CalculateDiscount(Order order) => order.TotalAmount * 0.1m;

        public bool IsApplicable(Order order) => order.Customer.Tier == CustomerTier.Gold;
    }
}
