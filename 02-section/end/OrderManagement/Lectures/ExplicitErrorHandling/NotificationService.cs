namespace OrderManagement.Lectures.ExplicitErrorHandling;

public class NotificationService
{
    public Result<Notification> SendDeliveryUpdate(Order order, Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(order);

        if (string.IsNullOrEmpty(customer.Email))
        {
            return Result<Notification>.Failure(new Error("Email is required."));
        }

        if (!customer.Email.Contains("@"))
        {
            return Result<Notification>.Failure(new Error("Email is invalid."));
        }

        // Send notification here
        return Result<Notification>.Success(
            new Notification(customer.Email, "Delivery Update"));
    }
}

public record Notification(string email, string subject);

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