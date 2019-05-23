using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Payment_API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment_API.Model
{
    public class MDRData
    {
        /// <summary>
        /// Stores all fees.
        /// </summary>
        private static ICollection<MDR> _fees;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        private MDRData() { }

        /// <summary>
        /// Gets all the fees.
        /// </summary>
        private static ICollection<MDR> Fees
        {
            get
            {
                if (_fees == null)
                    _fees = InitializeFees();
                return _fees;
            }
        }

        /// <summary>
        /// Fills the fees values.
        /// </summary>
        private static ICollection<MDR> InitializeFees()
        {
            var fees = new List<MDR>();
            MDR mdr;

            mdr = new MDR("A");
            mdr.Fees.Add(new Fee(CardBrand.Visa, 2.25, 2.00));
            mdr.Fees.Add(new Fee(CardBrand.Mastercard, 2.35, 1.98));
            fees.Add(mdr);

            mdr = new MDR("B");
            mdr.Fees.Add(new Fee(CardBrand.Visa, 2.50, 2.08));
            mdr.Fees.Add(new Fee(CardBrand.Mastercard, 2.65, 1.75));
            fees.Add(mdr);

            mdr = new MDR("C");
            mdr.Fees.Add(new Fee(CardBrand.Visa, 2.75, 2.16));
            mdr.Fees.Add(new Fee(CardBrand.Mastercard, 3.10, 1.58));
            fees.Add(mdr);

            return fees;
        }

        /// <summary>
        /// Gets the fee value as decimal (eg. 12.54%).
        /// </summary>
        /// <param name="acquirer">Acquirer code.</param>
        /// <param name="cardBrand">Card brand.</param>
        /// <param name="transactionType">Transaction type.</param>
        /// <returns>Fee value as decimal (eg. 12.54%).</returns>
        public static double GetFee(string acquirer, CardBrand cardBrand, TransactionType transactionType)
        {
            if (string.IsNullOrEmpty(acquirer)) throw new ArgumentNullException(nameof(acquirer));

            if (!Fees.Any(e => e.Acquirer.Equals(acquirer))) throw new ArgumentOutOfRangeException("Invalid Acquirer.");

            var mdr = Fees.Single(e => e.Acquirer.Equals(acquirer));
            var fee = mdr.Fees.Single(e => e.CardBrand == cardBrand);

            switch (transactionType)
            {
                case TransactionType.Credit:
                    return fee.Credit;
                case TransactionType.Debit:
                    return fee.Debit;
                default:
                    throw new IndexOutOfRangeException("Invalid Transaction Type.");
            }
        }

        /// <summary>
        /// Gets all the fees and MDR information.
        /// </summary>
        /// <returns>A collection of MDR.</returns>
        public static ICollection<MDR> GetMDR()
        {
            return Fees;
        }
    }

    public class MDR
    {
        [JsonProperty("Adquirente")]
        public string Acquirer { get; private set; }
        [JsonProperty("Taxas")]
        public ICollection<Fee> Fees { get; private set; }

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="acquirer"/>.
        /// </summary>
        /// <param name="acquirer">Acquirer.</param>
        public MDR(string acquirer)
        {
            if (string.IsNullOrEmpty(acquirer)) throw new ArgumentNullException(nameof(acquirer));

            this.Acquirer = acquirer;
            this.Fees = new List<Fee>();
        }
    }
    public class Fee
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Bandeira")]
        public CardBrand CardBrand { get; set; }

        [JsonProperty("Credito")]
        public double Credit { get; set; }

        [JsonProperty("Debito")]
        public double Debit { get; set; }

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        public Fee() { }

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="cardBrand"/>, <paramref name="creditFee"/> and <paramref name="debitFee"/>.
        /// </summary>
        /// <param name="cardBrand">Card brand.</param>
        /// <param name="creditFee">Credit fee as decimal (eg. 12.54%).</param>
        /// <param name="debitFee">Debit fee as decimal (eg. 12.54%).</param>
        public Fee(CardBrand cardBrand, double creditFee, double debitFee)
        {
            this.CardBrand = cardBrand;
            this.Credit = creditFee;
            this.Debit = debitFee;
        }
    }
}
