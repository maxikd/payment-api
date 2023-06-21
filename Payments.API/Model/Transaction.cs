using System.Text.Json.Serialization;

namespace Payments.API.Model
{
    public class Transaction
    {
        [JsonPropertyName("Valor")]
        [JsonRequired]
        public double Amount { get; set; }

        [JsonPropertyName("Tipo")]
        [JsonRequired]
        public string TransactionType { get; set; }

        [JsonPropertyName("Bandeira")]
        [JsonRequired]
        public string CardBrand { get; set; }

        [JsonPropertyName("Adquirente")]
        [JsonRequired]
        public string Acquirer { get; set; }

        /// <summary>
        /// Initializes an empty intance.
        /// </summary>
        public Transaction() { }

        /// <summary>
        /// Initializes an instance with the specified <paramref name="amount"/>, <paramref name="acquirer"/>, 
        /// <paramref name="cardBrand"/> and <paramref name="transactionType"/>.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="acquirer"></param>
        /// <param name="cardBrand"></param>
        /// <param name="transactionType"></param>
        public Transaction(double amount, string acquirer, string cardBrand, string transactionType)
        {
            this.Amount = amount;
            this.Acquirer = acquirer;
            this.CardBrand = cardBrand;
            this.TransactionType = transactionType;
        }
    }

    public class NetAmount
    {
        [JsonPropertyName("ValorLiquido")]
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