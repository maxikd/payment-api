using System;

namespace Payments.API.Mappers.Requests;

public class TransactionTypeMapper : IMapper<Contracts.Enums.TransactionType, Entities.Enums.TransactionType>
{
    public Entities.Enums.TransactionType Map(
        Contracts.Enums.TransactionType input)
    {
        return input switch
        {
            Contracts.Enums.TransactionType.Credito => Entities.Enums.TransactionType.Credit,
            Contracts.Enums.TransactionType.Debito => Entities.Enums.TransactionType.Debit,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Invalid transaction type")
        };
    }
}