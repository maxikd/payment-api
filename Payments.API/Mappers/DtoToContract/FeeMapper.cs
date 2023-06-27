using System;
using Payments.API.Contracts;
using Payments.API.Dtos;

namespace Payments.API.Mappers.DtoToContract;

public class FeeMapper : IMapper<FeeDto, PaymentFee>
{
    public FeeMapper(
        IMapper<Dtos.Enums.CardBrand, Contracts.Enums.CardBrand> cardBrandMapper)
    {
        CardBrandMapper = cardBrandMapper ?? throw new ArgumentNullException(nameof(cardBrandMapper));
    }

    public IMapper<Dtos.Enums.CardBrand, Contracts.Enums.CardBrand> CardBrandMapper { get; }

    public PaymentFee Map(
        FeeDto input)
    {
        var fee = new PaymentFee
        {
            CardBrand = CardBrandMapper.Map(input.CardBrand),
            Credit = input.CreditFee,
            Debit = input.DebitFee
        };

        return fee;
    }
}