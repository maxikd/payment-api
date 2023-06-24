using System;
using Payments.API.Contracts;
using Payments.API.Entities;

namespace Payments.API.Mappers.Responses;

public class FeeMapper : IMapper<Fee, PaymentFee>
{
    public PaymentFee Map(
        Fee input)
    {
        var fee = new PaymentFee
        {
            CardBrand = MapCardBrand(input.CardBrand),
            Credit = input.CreditFee,
            Debit = input.DebitFee
        };

        return fee;
    }

    private static Contracts.Enums.CardBrand MapCardBrand(
        Entities.Enums.CardBrand brand)
    {
        return brand switch
        {
            Entities.Enums.CardBrand.Mastercard => Contracts.Enums.CardBrand.Master,
            Entities.Enums.CardBrand.Visa => Contracts.Enums.CardBrand.Visa,
            _ => throw new ArgumentOutOfRangeException(nameof(brand), brand, "Invalid card brand")
        };
    }
}