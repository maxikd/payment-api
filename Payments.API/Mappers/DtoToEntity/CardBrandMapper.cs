using System;

namespace Payments.API.Mappers.DtoToEntity;

public class CardBrandMapper : IMapper<Dtos.Enums.CardBrand, Entities.Enums.CardBrand>
{
    public Entities.Enums.CardBrand Map(
        Dtos.Enums.CardBrand input)
    {
        return input switch
        {
            Dtos.Enums.CardBrand.Visa => Entities.Enums.CardBrand.Visa,
            Dtos.Enums.CardBrand.Mastercard => Entities.Enums.CardBrand.Mastercard,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Invalid card brand")
        };
    }
}