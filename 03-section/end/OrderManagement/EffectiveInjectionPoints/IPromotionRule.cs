namespace OrderManagement.EffectiveInjectionPoints
{
    public interface IPromotionRule
    {
        public bool IsApplicable(Order order);

        public decimal CalculateDiscount(Order order);
    }
}