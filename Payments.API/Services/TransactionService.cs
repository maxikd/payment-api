using System;
using Payments.API.Mappers;
using Payments.API.Repositories.Abstractions;
using Payments.API.Services.Abstractions;

namespace Payments.API.Services;

public class TransactionService : ITransactionService
{
    public TransactionService(
        IMdrRepository mdrRepository,
        IMapper<Dtos.Enums.CardBrand, Entities.Enums.CardBrand> cardBrandMapper,
        IMapper<Dtos.Enums.TransactionType, Entities.Enums.TransactionType> transactionTypeMapper)
    {
        MdrRepository = mdrRepository ?? throw new ArgumentNullException(nameof(mdrRepository));
        CardBrandMapper = cardBrandMapper ?? throw new ArgumentNullException(nameof(cardBrandMapper));
        TransactionTypeMapper = transactionTypeMapper ?? throw new ArgumentNullException(nameof(transactionTypeMapper));
    }

    public IMdrRepository MdrRepository { get; }
    public IMapper<Dtos.Enums.CardBrand, Entities.Enums.CardBrand> CardBrandMapper { get; }
    public IMapper<Dtos.Enums.TransactionType, Entities.Enums.TransactionType> TransactionTypeMapper { get; }

    public double Execute(
        double amount,
        string acquirer,
        Dtos.Enums.CardBrand cardBrand,
        Dtos.Enums.TransactionType transactionType)
    {
        var brand = CardBrandMapper.Map(cardBrand);
        var type = TransactionTypeMapper.Map(transactionType);

        var net = ComputeNetAmount(
            amount,
            acquirer,
            brand,
            type);

        return net;
    }

    private static double ComputeNetAmount(
        double amount,
        double percentage)
    {
        double liquid, fee;

        fee = amount * percentage / 100;
        liquid = amount - fee;

        return liquid;
    }

    private double ComputeNetAmount(
        double amount,
        string acquirer,
        Entities.Enums.CardBrand cardBrand,
        Entities.Enums.TransactionType transactionType)
    {
        double percentage = MdrRepository.GetFee(
            acquirer,
            cardBrand,
            type: transactionType);

        return ComputeNetAmount(amount, percentage);
    }
}