using System;
using System.Collections.Generic;
using Payments.API.Contracts;
using Payments.API.Dtos;

namespace Payments.API.Mappers.DtoToContract;

public class MdrMapper : IMapper<MdrDto, PaymentMdr>
{
    public MdrMapper(
        IMapper<IEnumerable<FeeDto>, IEnumerable<PaymentFee>> feesMapper)
    {
        FeesMapper = feesMapper ?? throw new ArgumentNullException(nameof(feesMapper));
    }

    public IMapper<IEnumerable<FeeDto>, IEnumerable<PaymentFee>> FeesMapper { get; }

    public PaymentMdr Map(
        MdrDto input)
    {
        var mdr = new PaymentMdr
        {
            Acquirer = input.Acquirer,
            Fees = FeesMapper.Map(input.Fees)
        };

        return mdr;
    }
}