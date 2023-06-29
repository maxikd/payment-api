using System;
using Payments.API.Dtos;
using Payments.API.Entities;

namespace Payments.API.Mappers.EntityToDto;

public class FeeMapper : IMapper<Fee, FeeDto>
{
    public FeeMapper(
        IMapper<Entities.Enums.CardBrand, Dtos.Enums.CardBrand> cardBrandMapper)
    {
        CardBrandMapper = cardBrandMapper ?? throw new ArgumentNullException(nameof(cardBrandMapper));
    }

    public IMapper<Entities.Enums.CardBrand, Dtos.Enums.CardBrand> CardBrandMapper { get; }

    public FeeDto Map(
        Fee input)
    {
        var cardBrand = CardBrandMapper.Map(input.CardBrand);

        var fee = new FeeDto(
            cardBrand,
            input.CreditFee,
            input.DebitFee);

        return fee;
    }
}