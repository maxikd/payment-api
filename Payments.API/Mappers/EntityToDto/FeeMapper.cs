using System;
using Payments.API.Dtos;
using Payments.API.Entities;

namespace Payments.API.Mappers.EntityToDto;

public class FeeMapper : IMapper<Fee, FeeDto>
{
    public FeeDto Map(
        Fee input)
    {
        var cardBrand = MapCardBrand(input.CardBrand);

        var fee = new FeeDto(
            cardBrand,
            input.CreditFee,
            input.DebitFee);

        return fee;
    }

    private static Dtos.Enums.CardBrand MapCardBrand(
        Entities.Enums.CardBrand brand)
    {
        return brand switch
        {
            Entities.Enums.CardBrand.Mastercard => Dtos.Enums.CardBrand.Mastercard,
            Entities.Enums.CardBrand.Visa => Dtos.Enums.CardBrand.Visa,
            _ => throw new ArgumentOutOfRangeException(nameof(brand), brand, "Invalid card brand")
        };
    }
}