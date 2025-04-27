namespace OrderManagement.DealWithConstructorBloat;

public class OrderProcessor
{
    private readonly OrderFinancialService _orderFinancialService;
    private readonly InventoryManager _inventoryManager;
    private readonly PaymentProcessor _paymentProcessor;
    private readonly OrderValidator _orderValidator;
    private readonly OrderNotificationService _orderNotificationService;

    public OrderProcessor(
        OrderFinancialService orderFinancialService,
        InventoryManager inventoryManager,
        PaymentProcessor paymentProcessor,
        OrderValidator orderValidator,
        OrderNotificationService orderNotificationService)
    {
        _orderFinancialService = orderFinancialService;
        _inventoryManager = inventoryManager;
        _paymentProcessor = paymentProcessor;
        _orderValidator = orderValidator;
        _orderNotificationService = orderNotificationService;
    }

    public OrderResult ProcessOrder(Order order)
    {
        // Validate order
        if (!_orderValidator.ValidateOrder(order))
        {
            return new OrderResult { Success = false, Error = "Invalid order" };
        }

        // Calculate financials
        var finalAmount = _orderFinancialService.Calculate(order);

        // Process payment
        var paymentSuccessful = _paymentProcessor.ProcessPayment(order, finalAmount);
        if (!paymentSuccessful)
        {
            return new OrderResult { Success = false, Error = "Payment failed" };
        }

        // Update inventory
        _inventoryManager.ReserveInventory(order);

        // Notify stakeholders
        _orderNotificationService.Notify(order);

        return new OrderResult { Success = true, OrderId = order.Id };
    }
}


public class OrderNotificationService
{
    private readonly CustomerNotifier _customerNotifier;
    private readonly WarehouseNotifier _warehouseNotifier;

    public OrderNotificationService(CustomerNotifier customerNotifier, WarehouseNotifier warehouseNotifier)
    {
        _customerNotifier = customerNotifier;
        _warehouseNotifier = warehouseNotifier;
    }

    public void Notify(Order order)
    {
        _customerNotifier.NotifyCustomer(order);
        _warehouseNotifier.NotifyWarehouse(order);
    }
}

public class OrderFinancialService
{
    private readonly DiscountCalculator _discountCalculator;
    private readonly ShippingCalculator _shippingCalculator;
    private readonly TaxCalculator _taxCalculator;

    public OrderFinancialService(
        DiscountCalculator discountCalculator,
        ShippingCalculator shippingCalculator,
        TaxCalculator taxCalculator)
    {
        _discountCalculator = discountCalculator;
        _shippingCalculator = shippingCalculator;
        _taxCalculator = taxCalculator;
    }

    public decimal Calculate(Order order)
    {
        var discount = _discountCalculator.CalculateDiscount(order);
        var shipping = _shippingCalculator.CalculateShipping(order);
        var tax = _taxCalculator.CalculateTax(order);
        return order.TotalAmount - discount + tax + shipping;
    }
}