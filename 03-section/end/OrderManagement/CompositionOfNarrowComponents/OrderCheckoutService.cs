namespace OrderManagement.CompositionOfNarrowComponents;

public interface IInventoryValidator
{
    bool HasSufficientInventory(int productId, int quantity);
}

public interface IProductRepository
{
    public int GetInventoryLevel(int productId) => 100;

    public Product GetProduct(int productId) => new Product { Price = 10.99m };
}

public interface IPriceCalculator
{
    public decimal Calculate(ShoppingCart cart);
}

public class InventoryValidator : IInventoryValidator
{
    private readonly IProductRepository _productRepository;

    public InventoryValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool HasSufficientInventory(int productId, int quantity) => _productRepository
        .GetInventoryLevel(productId) >= quantity;
}

public class PriceCalculator : IPriceCalculator
{
    private readonly IProductRepository _productRepository;

    public PriceCalculator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public decimal Calculate(ShoppingCart cart)
    {
        decimal total = 0;
        foreach (var item in cart.Items)
        {
            var product = _productRepository.GetProduct(item.ProductId);
            total += product.Price * item.Quantity;
        }
        return total;
    }
}

public class OrderCheckoutService
{
    private readonly IInventoryValidator _inventoryValidator;
    private readonly IPriceCalculator _priceCalculator;

    public OrderCheckoutService(IInventoryValidator inventoryValidator, IPriceCalculator priceCalculator)
    {
        _inventoryValidator = inventoryValidator;
        _priceCalculator = priceCalculator;
    }

    public OrderResult Checkout(ShoppingCart cart, PaymentInfo paymentInfo)
    {
        foreach (var item in cart.Items)
        {
            if (_inventoryValidator.HasSufficientInventory(item.Quantity, item.ProductId))
            {
                return new OrderResult { Success = false, Error = "Not enough inventory" };
            }
        }

        decimal totalPrice = _priceCalculator.Calculate(cart);

        try
        {
            ProcessPayment(paymentInfo, totalPrice);
        }
        catch (Exception ex)
        {
            return new OrderResult { Success = false, Error = ex.Message };
        }

        var order = CreateOrder(cart, totalPrice);

        return new OrderResult { Success = true, OrderId = order.Id };
    }

    // Helper methods
    private void ProcessPayment(PaymentInfo paymentInfo, decimal amount) { }
    private Order CreateOrder(ShoppingCart cart, decimal total) => new Order { Id = 123 };
}