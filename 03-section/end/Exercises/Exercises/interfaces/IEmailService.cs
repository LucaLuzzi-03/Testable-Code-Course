
namespace Exercises.interfaces
{
    public interface IEmailService
    {
        void SendOrderConfirmation(string email, Order order);
    }
}
