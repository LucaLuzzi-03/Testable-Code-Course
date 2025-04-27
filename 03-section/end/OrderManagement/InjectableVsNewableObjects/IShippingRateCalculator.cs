namespace OrderManagement.InjectableVsNewableObjects
{
    public interface IShippingRateCalculator
    {
        public decimal CalculateRate(Address shippingAddress, decimal weight);
    }
}