using Payment_API.Enums;
using Payment_API.Model;
using System;

namespace Payment_API.Helpers
{
    public static class Amounts
    {
        /// <summary>
        /// Computes the net amount based on <paramref name="percentage"/>.
        /// </summary>
        /// <param name="amount">Transaction amount.</param>
        /// <param name="percentage">Percentage value as decimal (eg. 12.54%).</param>
        /// <returns>Net amount.</returns>
        private static double ComputeNetAmount(double amount, double percentage)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), amount, "Value must be greater than 0.");
            if (percentage <= 0) throw new ArgumentOutOfRangeException(nameof(percentage), percentage, "Value must be greater than 0.");

            double liquid, fee;

            fee = amount * percentage / 100;
            liquid = amount - fee;

            return liquid;
        }

        /// <summary>
        /// Computes the net amount based on <paramref name="acquirer"/>, <paramref name="cardBrand"/> and <paramref name="transactionType"/>.
        /// </summary>
        /// <param name="amount">Transaction amount.</param>
        /// <param name="acquirer">Acquirer.</param>
        /// <param name="cardBrand">Card brand of the transaction.</param>
        /// <param name="transactionType">Transaction type.</param>
        /// <returns>Net amount.</returns>
        public static double ComputeNetAmount(double amount, string acquirer, string cardBrand, string transactionType)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), amount, "Value must be greater than 0.");
            if (string.IsNullOrEmpty(acquirer)) throw new ArgumentNullException(nameof(acquirer), "Acquirer can't be null.");

            double percentage = MDRData.GetFee(acquirer, cardBrand, transactionType);

            return ComputeNetAmount(amount, percentage);
        }

        /// <summary>
        /// Computes the net amount for the <paramref name="transaction"/>.
        /// </summary>
        /// <param name="transaction">Transaction object.</param>
        /// <returns>Net amount.</returns>
        public static double ComputeNetAmount(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction), "Transaction can't be null.");
            if (string.IsNullOrEmpty(transaction.CardBrand)) throw new ArgumentNullException(nameof(transaction.CardBrand), "Card brand can't be null.");
            if (string.IsNullOrEmpty(transaction.TransactionType)) throw new ArgumentNullException(nameof(transaction.TransactionType), "Transaction type can't be null.");

            return ComputeNetAmount(transaction.Amount, transaction.Acquirer, transaction.CardBrand, transaction.TransactionType);
        }
    }
}
