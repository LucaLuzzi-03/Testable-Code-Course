namespace OrderManagement.IdentifyExtractResponsibilities
{
    public static class CustomerLoyaltyPointsManager
    {
        public static void UpdateCustomerLoyaltyPoints(Order order)
        {
            int points = (int)(order.TotalAmount / 10);
            order.Customer.LoyaltyPoints += points;

            if (order.Customer.LoyaltyPoints > 1000)
                order.Customer.LoyaltyTier = LoyaltyTier.Gold;
            else if (order.Customer.LoyaltyPoints > 500)
                order.Customer.LoyaltyTier = LoyaltyTier.Silver;
        }
    }
}