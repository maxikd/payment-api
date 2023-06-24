using System;

namespace Payments.API.Mappers.Requests;

public class CardBrandMapper : IMapper<Contracts.Enums.CardBrand, Entities.Enums.CardBrand>
{
    public Entities.Enums.CardBrand Map(
        Contracts.Enums.CardBrand input)
    {
        return input switch
        {
            Contracts.Enums.CardBrand.Visa => Entities.Enums.CardBrand.Visa,
            Contracts.Enums.CardBrand.Master => Entities.Enums.CardBrand.Mastercard,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Invalid card brand")
        };
    }
}