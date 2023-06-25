using Payments.API.Contracts;

namespace Payments.API.Mappers.DtoToContract;

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