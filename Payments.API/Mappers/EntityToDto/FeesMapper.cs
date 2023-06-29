using System.Collections.Generic;
using System.Linq;
using Payments.API.Dtos;
using Payments.API.Entities;

namespace Payments.API.Mappers.EntityToDto;

public class FeesMapper : IMapper<IEnumerable<Fee>, IEnumerable<FeeDto>>
{
    public FeesMapper(
        IMapper<Fee, FeeDto> feeMapper)
    {
        FeeMapper = feeMapper ?? throw new System.ArgumentNullException(nameof(feeMapper));
    }

    public IMapper<Fee, FeeDto> FeeMapper { get; }

    public IEnumerable<FeeDto> Map(
        IEnumerable<Fee> input)
    {
        return input.Select(FeeMapper.Map);
    }
}