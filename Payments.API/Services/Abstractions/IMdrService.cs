using System.Collections.Generic;
using Payments.API.Entities;

namespace Payments.API.Services.Abstractions;

public interface IMdrService
{
    IEnumerable<Mdr> GetAll();
}