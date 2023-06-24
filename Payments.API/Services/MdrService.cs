using System;
using System.Collections.Generic;
using Payments.API.Entities;
using Payments.API.Repositories.Abstractions;
using Payments.API.Services.Abstractions;

namespace Payments.API.Services;

public class MdrService : IMdrService
{
    public MdrService(
        IMdrRepository mdrRepository)
    {
        MdrRepository = mdrRepository ?? throw new ArgumentNullException(nameof(mdrRepository));
    }

    public IMdrRepository MdrRepository { get; }

    public IEnumerable<Mdr> GetAll()
    {
        var mdrEntities = MdrRepository.GetAll();

        return mdrEntities;
    }
}