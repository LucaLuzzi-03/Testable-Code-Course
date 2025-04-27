
using Exercises.interfaces;

namespace Exercises
{
    public class CustomerRepository : ICustomerRepository
    {
        public void SaveCustomer(Customer customer)
        {
            // In a real implementation, this would save to a database
            Console.WriteLine($"Saving customer {customer.Id} to repository");
        }
    }
}
