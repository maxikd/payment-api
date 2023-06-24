using System.Collections.Generic;
using Payments.API.Entities;
using Payments.API.Entities.Enums;

namespace Payments.API.Repositories.Abstractions;

public interface IMdrRepository
{
    IEnumerable<Mdr> GetAll();

    double GetFee(
        string acquirer,
        CardBrand brand,
        TransactionType type);
}