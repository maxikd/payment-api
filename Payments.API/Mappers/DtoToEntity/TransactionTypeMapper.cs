using System;

namespace Payments.API.Mappers.DtoToEntity;

public class TransactionTypeMapper : IMapper<Dtos.Enums.TransactionType, Entities.Enums.TransactionType>
{
    public Entities.Enums.TransactionType Map(
        Dtos.Enums.TransactionType input)
    {
        return input switch
        {
            Dtos.Enums.TransactionType.Credit => Entities.Enums.TransactionType.Credit,
            Dtos.Enums.TransactionType.Debit => Entities.Enums.TransactionType.Debit,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "Invalid transaction type")
        };
    }
}