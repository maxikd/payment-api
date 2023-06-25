using System;
using System.Collections.Generic;
using Payments.API.Dtos;
using Payments.API.Entities;

namespace Payments.API.Mappers.EntityToDto;

public class MdrMapper : IMapper<Mdr, MdrDto>
{
    public MdrMapper(
        IMapper<IEnumerable<Fee>, IEnumerable<FeeDto>> feeMapper)
    {
        FeeMapper = feeMapper ?? throw new ArgumentNullException(nameof(feeMapper));
    }

    public IMapper<IEnumerable<Fee>, IEnumerable<FeeDto>> FeeMapper { get; }

    public MdrDto Map(
        Mdr input)
    {
        var mdr = new MdrDto(
            input.Acquirer,
            FeeMapper.Map(input.Fees));

        return mdr;
    }
}