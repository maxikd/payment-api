using System.Collections.Generic;
using Payments.API.Dtos;

namespace Payments.API.Services.Abstractions;

public interface IMdrService
{
    IEnumerable<MdrDto> GetAll();
}