using System;

namespace Payments.API.Mappers.ContractToDto;

public class TransactionTypeMapper : IMapper<Contracts.Enums.TransactionType, Dtos.Enums.TransactionType>
{
    public Dtos.Enums.TransactionType Map(
        Contracts.Enums.TransactionType input)
    {
        return input switch
        {
            Contracts.Enums.TransactionType.Credito => Dtos.Enums.TransactionType.Credit,
            Contracts.Enums.TransactionType.Debito => Dtos.Enums.TransactionType.Debit,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Invalid transaction type")
        };
    }
}