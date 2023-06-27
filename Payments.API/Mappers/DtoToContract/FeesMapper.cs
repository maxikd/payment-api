using System.Collections.Generic;
using System.Linq;
using Payments.API.Contracts;
using Payments.API.Dtos;

namespace Payments.API.Mappers.DtoToContract;

public class FeesMapper : IMapper<IEnumerable<FeeDto>, IEnumerable<PaymentFee>>
{
    public FeesMapper(IMapper<FeeDto, PaymentFee> feeMapper)
    {
        FeeMapper = feeMapper ?? throw new System.ArgumentNullException(nameof(feeMapper));
    }

    public IMapper<FeeDto, PaymentFee> FeeMapper { get; }

    public IEnumerable<PaymentFee> Map(
        IEnumerable<FeeDto> input)
    {
        return input.Select(FeeMapper.Map);
    }
}