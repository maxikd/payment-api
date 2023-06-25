using System;

namespace Payments.API.Mappers.ContractToDto;

public class CardBrandMapper : IMapper<Contracts.Enums.CardBrand, Dtos.Enums.CardBrand>
{
    public Dtos.Enums.CardBrand Map(
        Contracts.Enums.CardBrand input)
    {
        return input switch
        {
            Contracts.Enums.CardBrand.Visa => Dtos.Enums.CardBrand.Visa,
            Contracts.Enums.CardBrand.Master => Dtos.Enums.CardBrand.Mastercard,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Invalid card brand")
        };
    }
}