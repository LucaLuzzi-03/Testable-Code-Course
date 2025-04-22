using OrderManagement;

namespace Exercises;

public enum StoreLocation
{
    Warehouse,
    Mall,
    Online
}

public class OrderLocation
{
    public StoreLocation Location { get; set; }
    public bool ExpressShippingAvailable { get; set; }

    public static OrderLocation GetOrderLocation(StoreLocation location)
    {
        return new OrderLocation
        {
            Location = location,
            ExpressShippingAvailable = (location == StoreLocation.Warehouse || location == StoreLocation.Mall)
        };
    }
}

public class OrderService
{
    private List<Product> _productInventory;
    private List<Order> _orders;
    private Customer _currentCustomer;

    public OrderService()
    {
        _productInventory = new List<Product>();
        _orders = new List<Order>();
    }

    public bool ProcessOrder(string orderId, OrderStatus status)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        ArgumentNullException.ThrowIfNull(order);

        order.Status = status;
        return true;
    }

    public Result<Order> CreateOrder(List<OrderItem> items, bool? priority = false)
    {

        if (_currentCustomer == null)
        {
            return Result<Order>.Failure(new Error("No customer selected"));
        }

        var order = new Order
        {
            Id = Guid.NewGuid().ToString(),
            CustomerId = _currentCustomer.Id,
            Items = items,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending
        };

        foreach (var item in items)
        {
            var product = _productInventory.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null)
            {
                continue;
            }

            if (product.StockQuantity < item.Quantity)
            {
                return Result<Order>.Failure(new Error($"Not enough stock for product {product.Name}"));
            }

            product.StockQuantity -= item.Quantity;
        }

        _orders.Add(order);
        return Result<Order>.Success(order);
    }

    public bool SetCustomer(string customerId)
    {
        var customer = FindCustomer(customerId);
        if (customer != null)
        {
            _currentCustomer = customer;
            return true;
        }

        return false;
    }

    private Customer FindCustomer(string customerId)
    {
        // This would typically be a database lookup
        return new Customer { Id = customerId, Name = "Test Customer" };
    }

    public object ApplyDiscount(string orderId, decimal discountPercent)
    {
        try
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return "Order not found";
            }

            if (discountPercent < 0 || discountPercent > 100)
            {
                return false;
            }

            // Apply discount logic...

            return order;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}











public readonly record struct Error(string message);
public readonly record struct Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }

    public bool isSuccess => Error is null;

    private Result(T value)
    {
        Value = value;
        Error = null;
    }

    private Result(Error error)
    {
        Value = default;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
}