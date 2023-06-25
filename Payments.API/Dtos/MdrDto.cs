using System.Collections.Generic;

namespace Payments.API.Dtos;

public record class MdrDto(
    string Acquirer,
    IEnumerable<FeeDto> Fees);