
namespace Exercises.interfaces
{
    public interface IInventoryManager
    {
        public void UpdateInventory(Order order);

        public Product? GetProductById(int productId);
    }
}
