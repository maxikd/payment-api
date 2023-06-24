using System;
using Payments.API.Entities.Enums;
using Payments.API.Repositories.Abstractions;
using Payments.API.Services.Abstractions;

namespace Payments.API.Services;

public class TransactionService : ITransactionService
{
    public TransactionService(
        IMdrRepository mdrRepository)
    {
        MdrRepository = mdrRepository ?? throw new ArgumentNullException(nameof(mdrRepository));
    }

    public IMdrRepository MdrRepository { get; }

    public double Execute(
        double amount,
        string acquirer,
        CardBrand cardBrand,
        TransactionType transactionType)
    {
        var net = ComputeNetAmount(
            amount,
            acquirer,
            cardBrand,
            transactionType);

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
        CardBrand cardBrand,
        TransactionType transactionType)
    {
        double percentage = MdrRepository.GetFee(
            acquirer,
            cardBrand,
            type: transactionType);

        return ComputeNetAmount(amount, percentage);
    }
}