using Newtonsoft.Json;

namespace Payment_API.Model
{
    public class Transaction
    {
        [JsonProperty("Valor")]
        [JsonRequired]
        public double Amount { get; set; }

        [JsonProperty("Tipo")]
        [JsonRequired]
        public string TransactionType { get; set; }

        [JsonProperty("Bandeira")]
        [JsonRequired]
        public string CardBrand { get; set; }

        [JsonProperty("Adquirente")]
        [JsonRequired]
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
