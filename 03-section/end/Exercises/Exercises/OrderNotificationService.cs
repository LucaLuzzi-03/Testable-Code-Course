using Exercises.interfaces;

namespace Exercises;

public class OrderNotificationService : IOrderNotificationService
{
    private readonly IEmailService _emailService;
    private readonly ILoyaltyPointsCalculator _loyaltyPointsCalculator;
    private readonly IPushNotificationService _pushNotificationService;

    public OrderNotificationService(
        IEmailService emailService, 
        ILoyaltyPointsCalculator loyaltyPointsCalculator, 
        IPushNotificationService pushNotificationService)
    {
        _emailService = emailService;
        _loyaltyPointsCalculator = loyaltyPointsCalculator;
        _pushNotificationService = pushNotificationService;
    }

    public void SendOrderNotifications(Order order)
    {
        _emailService.SendOrderConfirmation(order.Customer.Email, order);
        int points = _loyaltyPointsCalculator.CalculatePoints(order);
        
        var notification = new OrderNotification
        {
            OrderId = order.Id,
            CustomerName = order.Customer.Name,
            TotalAmount = order.TotalAmount,
            LoyaltyPointsEarned = points,
            OrderDate = DateTime.Now 
        };
        
        _pushNotificationService.SendNotification(order.Customer.Id, notification);
    }
}