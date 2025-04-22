using Exercises;

namespace OrderManagement.Tests
{
    public class OrderServiceTests
    {
        [Theory]
        [InlineData(StoreLocation.Warehouse)]
        [InlineData(StoreLocation.Mall)]
        public void Should_return_express_shipping_available_when_location_is_warehouse_or_mall(StoreLocation storeLocation)
        {
            var orderLocation = OrderLocation.GetOrderLocation(storeLocation);
            Assert.True(orderLocation.ExpressShippingAvailable);
        }

        [Fact]
        public void Should_return_express_shipping_not_available_when_location_is_online()
        {
            var orderLocation = OrderLocation.GetOrderLocation(StoreLocation.Online);
            Assert.False(orderLocation.ExpressShippingAvailable);
        }

        [Fact]
        public void Should_not_process_order_when_order_does_not_exist()
        {
            var orderService = new OrderService();
            var orderId = "1";
            var status = OrderStatus.Processing;

            Assert.Throws<ArgumentNullException>(() 
                => orderService.ProcessOrder(orderId, status));
        }
    }
}
