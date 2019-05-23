using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Payment_API.Enums;

namespace Payment_API.Model
{
    public class Transaction
    {
        [JsonProperty("Valor")]
        public double Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Tipo")]
        public TransactionType? TransactionType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Bandeira")]
        public CardBrand? CardBrand { get; set; }

        [JsonProperty("Adquirente")]
        public string Acquirer { get; set; }
    }

    public class NetAmount
    {
        [JsonProperty("ValorLiquido")]
        public double Net { get; set; }

        /// <summary>
        /// Initializes an empty intance.
        /// </summary>
        public NetAmount() { }

        /// <summary>
        /// Initializes an instance with the specified <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount"></param>
        public NetAmount(double amount)
        {
            this.Net = amount;
        }
    }
}
