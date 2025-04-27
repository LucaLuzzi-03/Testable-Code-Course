using Exercises.enums;
using Exercises.interfaces;

namespace Exercises;

public class OrderService
{
    private readonly IOrderFinancialService _orderFinancialService;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IOrderNotificationService _orderNotificationService;
    private readonly IInventoryManager _inventoryManager;
    private readonly ICustomerLoyaltyManager _customerLoyaltyManager;
    private readonly List<Order> _orders;

    public OrderService(
        IOrderFinancialService orderFinancialService,
        IPaymentProcessor paymentProcessor,
        IOrderNotificationService orderNotificationService,
        IInventoryManager inventoryManager,
        ICustomerLoyaltyManager customerLoyaltyManager,
        List<Order> orders)
    {
        _orderFinancialService = orderFinancialService;
        _paymentProcessor = paymentProcessor;
        _orderNotificationService = orderNotificationService;
        _inventoryManager = inventoryManager;
        _customerLoyaltyManager = customerLoyaltyManager;
        _orders = orders;
    }
    
    public OrderResult ProcessOrder(Order order, PaymentInfo paymentInfo)
    {
        if (!IsInventoryValid(order))
        {
            return OrderFailure(order, $"Insufficient inventory");
        }

        decimal totalPrice = _orderFinancialService.CalculateOrderTotal(order);

        try
        {
            if (!_paymentProcessor.ProcessPayment(paymentInfo, totalPrice))
            {
                return OrderFailure(order, "Payment processing failed.");
            }
        }
        catch (Exception ex)
        {
            return OrderFailure(order, ex.Message);
        }

        _inventoryManager.UpdateInventory(order);
        _customerLoyaltyManager.UpdateCustomerLoyalty(order);

        FinalizeOrder(order, totalPrice);
        _orders.Add(order);

        _orderNotificationService.SendOrderNotifications(order);

        return OrderSuccess(order);
    }

    private bool IsInventoryValid(Order order)
    {
        foreach (var item in order.Items)
        {
            var product = _inventoryManager.GetProductById(item.ProductId);
            if (product == null || product.StockQuantity < item.Quantity)
            {
                return false;
            }
        }

        return true;
    }

    private static void FinalizeOrder(Order order, decimal totalPrice)
    {
        order.TotalAmount = totalPrice;
        order.OrderDate = DateTime.Now;
        order.Status = OrderStatus.COMPLETED;
    }

    private static OrderResult OrderSuccess(Order order)
    {
        return new OrderResult
        {
            Success = true,
            OrderId = order.Id
        };
    }

    private static OrderResult OrderFailure(Order order, string error)
    {
        return new OrderResult
        {
            Success = false,
            Error = error
        };
    }
}