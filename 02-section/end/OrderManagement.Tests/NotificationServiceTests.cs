namespace OrderManagement.Tests
{
    public class NotificationServiceTests
    {
        [Fact]
        public void Should_return_false_when_customer_opt_out_from_emails()
        {
            var notificationService = new Lectures.ClearInputsAndOutputs.NotificationService();
            var customer = new Customer()
            {
                OptOutEmail = true
            };

            var notificationWasSent = notificationService
                .SendNotification(customer, Lectures.ClearInputsAndOutputs.NotificationType.Email, "content", "header");

            Assert.False(notificationWasSent);
        }

        [Fact]
        public void Should_return_false_when_email_is_empty()
        {
            var notificationService = new Lectures.ExplicitErrorHandling.NotificationService();
            var customer = new Customer()
            {
                Email = ""
            };
            var order = new Order();

            var notificationWasSent = notificationService
                .SendDeliveryUpdate(order, customer);

            Assert.False(notificationWasSent.isSuccess);
            Assert.Equal("Email is required.", notificationWasSent.Error!.Value.message);
        }

        [Fact]
        public void Should_return_false_when_email_is_invalid()
        {
            var notificationService = new Lectures.ExplicitErrorHandling.NotificationService();
            var customer = new Customer()
            {
                Email = "locagmail.com"
            };
            var order = new Order();

            var notificationWasSent = notificationService
                .SendDeliveryUpdate(order, customer);

            Assert.False(notificationWasSent.isSuccess);
            Assert.Equal("Email is invalid.", notificationWasSent.Error!.Value.message);
        }

        [Fact]
        public void Should_return_argument_null_exception_when_order_is_null()
        {
            var notificationService = new Lectures.ExplicitErrorHandling.NotificationService();
            var customer = new Customer();

            var exception = Assert.Throws<ArgumentNullException>(() => notificationService
                .SendDeliveryUpdate(null, customer));
            Assert.Equal("order", exception.ParamName);
        }

        [Fact]
        public void Should_return_argument_null_exception_when_customer_is_null()
        {
            var notificationService = new Lectures.ExplicitErrorHandling.NotificationService();
            var order = new Order();

            var exception = Assert.Throws<ArgumentNullException>(() => notificationService
                .SendDeliveryUpdate(order, null));
            Assert.Equal("customer", exception.ParamName);
        }

        [Fact]
        public void Should_send_as_html_when_using_exchange()
        {
            var format = Lectures.SeparationOfConstructionAndLogic.NotificationFormat.Create("Exchange");

            Assert.True(format.Html);
        }

        [Fact]
        public void Should_disable_notifications()
        {
            var notificationManager = new Lectures.VisibleStateChanges.NotificationManager();
            notificationManager.DisableNotifications();

            Assert.False(notificationManager.NotificationsEnabled);
        }
    }
}
