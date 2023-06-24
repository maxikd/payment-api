using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Payments.API.Contracts;

public class PaymentMdr
{
    [JsonPropertyName("Adquirente")]
    public string Acquirer { get; init; }

    [JsonPropertyName("Taxas")]
    public IEnumerable<PaymentFee> Fees { get; init; }
}