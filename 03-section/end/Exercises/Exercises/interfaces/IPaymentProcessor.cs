
namespace Exercises.interfaces
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(PaymentInfo paymentInfo, decimal amount);
    }
}
