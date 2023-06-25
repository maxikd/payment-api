using System;
using System.Collections.Generic;
using System.Linq;
using Payments.API.Dtos;
using Payments.API.Entities;
using Payments.API.Mappers;
using Payments.API.Repositories.Abstractions;
using Payments.API.Services.Abstractions;

namespace Payments.API.Services;

public class MdrService : IMdrService
{
    public MdrService(
        IMdrRepository mdrRepository,
        IMapper<Mdr, MdrDto> dtoMapper)
    {
        MdrRepository = mdrRepository ?? throw new ArgumentNullException(nameof(mdrRepository));
        DtoMapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
    }

    public IMdrRepository MdrRepository { get; }
    public IMapper<Mdr, MdrDto> DtoMapper { get; }

    public IEnumerable<MdrDto> GetAll()
    {
        var mdrEntities = MdrRepository.GetAll();

        var mdrDtos = mdrEntities.Select(entity => DtoMapper.Map(entity));

        return mdrDtos;
    }
}