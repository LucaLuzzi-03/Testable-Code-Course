using Exercises.interfaces;

namespace Exercises.rules
{
    public class HolidayDiscountRule : IDiscountOrderRule
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public HolidayDiscountRule(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public decimal CalculateDiscount(Order order) => order.TotalAmount * 0.05m;

        public bool IsApplicable(Order order) => _dateTimeProvider.Now.Month == 12;
    }
}
