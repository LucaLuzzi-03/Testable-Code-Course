namespace OrderManagement.EffectiveInjectionPoints;

public class DiscountService
{
    private readonly IEnumerable<IPromotionRule> _promotionRules;

    public DiscountService(IEnumerable<IPromotionRule> promotionRules)
    {
        _promotionRules = promotionRules;
    }

    public decimal CalculateDiscount(Order order)
    {
        decimal discount = 0;

        foreach (var rule in _promotionRules)
        {
            if (rule.IsApplicable(order))
            {
                discount += rule.CalculateDiscount(order);
            }
        }

        return discount;
    }
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}

// Strategy Pattern
// ------------------------------------------------------------------------------------
public class HolidayPromotionRule : IPromotionRule
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public HolidayPromotionRule(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public decimal CalculateDiscount(Order order) => order.TotalAmount * 0.1m;

    public bool IsApplicable(Order order) => _dateTimeProvider.Now.Month == 12;
}

public class LargeOrderPromotionRule : IPromotionRule
{
    public decimal CalculateDiscount(Order order) => order.TotalAmount * 0.05m;
    public bool IsApplicable(Order order) => order.TotalAmount > 1000;
}
// ------------------------------------------------------------------------------------