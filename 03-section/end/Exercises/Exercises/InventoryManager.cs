using Exercises.interfaces;

namespace Exercises
{
    public class InventoryManager : IInventoryManager
    {
        private readonly List<Product> _inventory;

        public InventoryManager(List<Product> inventory)
        {
            _inventory = inventory;
        }

        public void UpdateInventory(Order order)
        {
            foreach (var item in order.Items)
            {
                var product = GetProductById(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;

                    if (product.StockQuantity < 10)
                    {
                        Console.WriteLine(
                            $"Low stock alert: Product {product.Id} has only {product.StockQuantity} units left.");
                    }
                }
            }
        }

        public Product? GetProductById(int productId) => _inventory.FirstOrDefault(p => p.Id == productId);
    }
}
