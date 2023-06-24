using Payments.API.Entities.Enums;

namespace Payments.API.Services.Abstractions;

public interface ITransactionService
{
    double Execute(
        double amount,
        string acquirer,
        CardBrand cardBrand,
        TransactionType transactionType);
}