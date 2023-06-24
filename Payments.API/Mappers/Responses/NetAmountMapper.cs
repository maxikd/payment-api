using Payments.API.Contracts;

namespace Payments.API.Mappers.Responses;

public class NetAmountMapper : IMapper<double, PaymentNetAmount>
{
    public PaymentNetAmount Map(
        double input)
    {
        var netAmount = new PaymentNetAmount
        {
            Net = input
        };

        return netAmount;
    }
}