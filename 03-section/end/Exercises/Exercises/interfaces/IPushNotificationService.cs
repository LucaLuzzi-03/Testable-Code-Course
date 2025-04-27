
namespace Exercises.interfaces
{
    public interface IPushNotificationService
    {
        void SendNotification(int customerId, OrderNotification notification);
    }
}
