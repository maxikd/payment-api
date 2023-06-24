using System.Text.Json.Serialization;
using Payments.API.Contracts.Enums;

namespace Payments.API.Contracts;

public class PaymentTransaction
{
    [JsonPropertyName("Valor")]
    [JsonRequired]
    public double Amount { get; set; }

    [JsonPropertyName("Tipo")]
    [JsonRequired]
    public TransactionType Type { get; set; }

    [JsonPropertyName("Bandeira")]
    [JsonRequired]
    public CardBrand CardBrand { get; set; }

    [JsonPropertyName("Adquirente")]
    [JsonRequired]
    public string Acquirer { get; set; }
}