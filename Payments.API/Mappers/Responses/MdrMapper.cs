using System;
using System.Collections.Generic;
using Payments.API.Contracts;
using Payments.API.Entities;

namespace Payments.API.Mappers.Responses;
public class MdrMapper : IMapper<Mdr, PaymentMdr>
{
    public MdrMapper(
        IMapper<IEnumerable<Fee>, IEnumerable<PaymentFee>> feeMapper)
    {
        FeeMapper = feeMapper ?? throw new ArgumentNullException(nameof(feeMapper));
    }

    public IMapper<IEnumerable<Fee>, IEnumerable<PaymentFee>> FeeMapper { get; }

    public PaymentMdr Map(
        Mdr input)
    {
        var mdr = new PaymentMdr
        {
            Acquirer = input.Acquirer,
            Fees = FeeMapper.Map(input.Fees)
        };

        return mdr;
    }
}