using System.Text.Json.Serialization;
using Payments.API.Contracts.Enums;

namespace Payments.API.Contracts;

public class PaymentFee
{
    [JsonPropertyName("Bandeira")]
    public CardBrand CardBrand { get; set; }

    [JsonPropertyName("Credito")]
    public double Credit { get; set; }

    [JsonPropertyName("Debito")]
    public double Debit { get; set; }
}