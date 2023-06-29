using Payments.API.Dtos;

namespace Payments.API.Services.Abstractions;

public interface ITransactionService
{
    double Execute(
        TransactionDto transaction);
}