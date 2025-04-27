using Exercises.interfaces;

namespace Exercises;

public class LoyaltyPointsCalculator : ILoyaltyPointsCalculator
{
    public int CalculatePoints(Order order) => (int)(order.TotalAmount / 10);
}