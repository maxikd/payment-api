using System.Text.Json.Serialization;

namespace Payments.API.Contracts;
public class PaymentNetAmount
{
    [JsonPropertyName("ValorLiquido")]
    public double Net { get; set; }
}