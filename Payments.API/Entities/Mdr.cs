using System.Collections.Generic;

namespace Payments.API.Entities;

public class Mdr
{
    public string Acquirer { get; init; }
    public IEnumerable<Fee> Fees { get; init; }
}