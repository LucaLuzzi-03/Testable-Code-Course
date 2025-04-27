namespace OrderManagement.EffectiveInjectionPoints
{
    public interface IDateTimeProvider
    {
        public DateTime Now { get; }
    }
}