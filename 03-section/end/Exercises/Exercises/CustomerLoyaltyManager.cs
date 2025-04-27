using Exercises.enums;
using Exercises.interfaces;

namespace Exercises
{
    public class CustomerLoyaltyManager : ICustomerLoyaltyManager
    {
        private readonly ILoyaltyPointsCalculator _loyaltyPointsCalculator;
        private readonly ICustomerRepository _customerRepository;

        public CustomerLoyaltyManager(
            ILoyaltyPointsCalculator loyaltyPointsCalculator,
            ICustomerRepository customerRepository)
        {
            _loyaltyPointsCalculator = loyaltyPointsCalculator;
            _customerRepository = customerRepository;
        }

        public void UpdateCustomerLoyalty(Order order)
        {
            // Calculate loyalty points
            int points = _loyaltyPointsCalculator.CalculatePoints(order);
            order.Customer.LoyaltyPoints += points;

            // Update customer tier based on points
            if (order.Customer.LoyaltyPoints > 1000)
            {
                order.Customer.Tier = CustomerTier.Gold;
            }
            else if (order.Customer.LoyaltyPoints > 500)
            {
                order.Customer.Tier = CustomerTier.Silver;
            }

            _customerRepository.SaveCustomer(order.Customer);
            Console.WriteLine($"Customer {order.Customer.Id} loyalty updated to {order.Customer.Tier}");
        }
    }
}
