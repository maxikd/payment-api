using System;
using Payments.API.Contracts;
using Payments.API.Dtos;

namespace Payments.API.Mappers.DtoToContract;

public class FeeMapper : IMapper<FeeDto, PaymentFee>
{
    public PaymentFee Map(
        FeeDto input)
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
        Dtos.Enums.CardBrand brand)
    {
        return brand switch
        {
            Dtos.Enums.CardBrand.Mastercard => Contracts.Enums.CardBrand.Master,
            Dtos.Enums.CardBrand.Visa => Contracts.Enums.CardBrand.Visa,
            _ => throw new ArgumentOutOfRangeException(nameof(brand), brand, "Invalid card brand")
        };
    }
}