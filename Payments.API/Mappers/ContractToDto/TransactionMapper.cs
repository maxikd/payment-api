using Payments.API.Contracts;
using Payments.API.Dtos;

namespace Payments.API.Mappers.ContractToDto;

public class TransactionMapper : IMapper<PaymentTransaction, TransactionDto>
{
    public TransactionMapper(
        IMapper<Contracts.Enums.CardBrand, Dtos.Enums.CardBrand> cardBrandMapper,
        IMapper<Contracts.Enums.TransactionType, Dtos.Enums.TransactionType> transactionTypeMapper)
    {
        CardBrandMapper = cardBrandMapper ?? throw new System.ArgumentNullException(nameof(cardBrandMapper));
        TransactionTypeMapper = transactionTypeMapper ?? throw new System.ArgumentNullException(nameof(transactionTypeMapper));
    }

    public IMapper<Contracts.Enums.CardBrand, Dtos.Enums.CardBrand> CardBrandMapper { get; }
    public IMapper<Contracts.Enums.TransactionType, Dtos.Enums.TransactionType> TransactionTypeMapper { get; }

    public TransactionDto Map(
        PaymentTransaction input)
    {
        var brand = CardBrandMapper.Map(input.CardBrand);
        var type = TransactionTypeMapper.Map(input.Type);

        var dto = new TransactionDto(
            input.Amount,
            input.Acquirer,
            brand,
            type);

        return dto;
    }
}