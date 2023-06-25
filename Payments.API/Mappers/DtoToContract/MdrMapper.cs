using System;
using System.Collections.Generic;
using Payments.API.Contracts;
using Payments.API.Dtos;

namespace Payments.API.Mappers.DtoToContract;

public class MdrMapper : IMapper<MdrDto, PaymentMdr>
{
    public MdrMapper(
        IMapper<IEnumerable<FeeDto>, IEnumerable<PaymentFee>> feeMapper)
    {
        FeeMapper = feeMapper ?? throw new ArgumentNullException(nameof(feeMapper));
    }

    public IMapper<IEnumerable<FeeDto>, IEnumerable<PaymentFee>> FeeMapper { get; }

    public PaymentMdr Map(
        MdrDto input)
    {
        var mdr = new PaymentMdr
        {
            Acquirer = input.Acquirer,
            Fees = FeeMapper.Map(input.Fees)
        };

        return mdr;
    }
}