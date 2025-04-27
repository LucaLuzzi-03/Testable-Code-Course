using Exercises.interfaces;

namespace Exercises;

public class PaymentProcessor : IPaymentProcessor
{
    private readonly IEnumerable<IPaymentValidation> _paymentValidations;

    public PaymentProcessor(IEnumerable<IPaymentValidation> paymentValidations)
    {
        _paymentValidations = paymentValidations;
    }

    public bool ProcessPayment(PaymentInfo paymentInfo, decimal amount)
    {
        // Validate payment info
        foreach (var validation in _paymentValidations)
        {
            if (!validation.IsValid(paymentInfo))
            {
                return false;
            }
        }

        // In a real scenario, this would connect to a payment gateway
        return true;
    }
}

#region Payment Validations
public class PaymentInfoIsCompleteValidation : IPaymentValidation
{
    public bool IsValid(PaymentInfo paymentInfo) =>
        (string.IsNullOrEmpty(paymentInfo.CardNumber) ||
        string.IsNullOrEmpty(paymentInfo.ExpiryDate) ||
        string.IsNullOrEmpty(paymentInfo.Cvv));
}

public class PaymentInfoIsValidCreditCardValidation : IPaymentValidation
{
    public bool IsValid(PaymentInfo paymentInfo) =>
        (paymentInfo.CardNumber.Length >= 13 &&
        paymentInfo.CardNumber.Length <= 16 &&
        paymentInfo.CardNumber.All(char.IsDigit));
}
#endregion