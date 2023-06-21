using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payments.API.Enums;
using Payments.API.Helpers;
using Payments.API.Model;
using System;

namespace Payments.API.UnitTests.Helpers
{
    [TestClass]
    public class AmountsTests
    {
        private const string ACQUIRER = "A";
        private const string INVALID_CARD_BRAND = "APPLE";
        private const string INVALID_TRANSACTION_TYPE = "PHONE";

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MDRData.GetMDR();
        }

        #region Exception tests
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Compute_Amount_0_ThrowsArgumentOutOfRangeException()
        {
            Amounts.ComputeNetAmount(0, ACQUIRER, CardBrand.Visa, TransactionType.Credit);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_Acquirer_Null_ThrowsArgumentNullException()
        {
            Amounts.ComputeNetAmount(100, null, CardBrand.Visa, TransactionType.Credit);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_Acquirer_Empty_ThrowsArgumentNullException()
        {
            Amounts.ComputeNetAmount(100, string.Empty, CardBrand.Visa, TransactionType.Credit);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_CardBrand_Null_ThrowsArgumentNullException()
        {
            Amounts.ComputeNetAmount(100, ACQUIRER, null, TransactionType.Credit);
        }
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Compute_CardBrand_Invalid_ThrowsArgumentOutOfRangeException()
        {
            Amounts.ComputeNetAmount(100, ACQUIRER, INVALID_CARD_BRAND, TransactionType.Credit);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_TransactionType_Null_ThrowsArgumentNullException()
        {
            Amounts.ComputeNetAmount(100, ACQUIRER, CardBrand.Visa, null);
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void Compute_TransactionType_Invalid_ThrowsIndexOutOfRangeException()
        {
            Amounts.ComputeNetAmount(100, ACQUIRER, CardBrand.Visa, INVALID_TRANSACTION_TYPE);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_Transaction_Null_ThrowsArgumentNullException()
        {
            Amounts.ComputeNetAmount(null);
        }
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Compute_TransactionAmount_0_ThrowsArgumentOutOfRangeException()
        {
            var transaction = new Transaction(0, ACQUIRER, CardBrand.Mastercard, TransactionType.Credit);

            Amounts.ComputeNetAmount(transaction);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_TransactionAcquirer_Null_ThrowsArgumentNullException()
        {
            var transaction = new Transaction(100, null, CardBrand.Mastercard, TransactionType.Credit);

            Amounts.ComputeNetAmount(transaction);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_TransactionAcquirer_Empty_ThrowsArgumentNullException()
        {
            var transaction = new Transaction(100, string.Empty, CardBrand.Mastercard, TransactionType.Credit);

            Amounts.ComputeNetAmount(transaction);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_TransactionCardBrand_Null_ThrowsArgumentNullException()
        {
            var transaction = new Transaction(100, ACQUIRER, null, TransactionType.Credit);

            Amounts.ComputeNetAmount(transaction);
        }
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Compute_TransactionCardBrand_Invalid_ThrowsArgumentOutOfRangeException()
        {
            var transaction = new Transaction(100, ACQUIRER, INVALID_CARD_BRAND, TransactionType.Credit);

            Amounts.ComputeNetAmount(transaction);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Compute_TransactionTransactionType_Null_ThrowsArgumentNullException()
        {
            var transaction = new Transaction(100, ACQUIRER, CardBrand.Mastercard, null);

            Amounts.ComputeNetAmount(transaction);
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void Compute_TransactionTransactionType_Invalid_ThrowsIndexOutOfRangeException()
        {
            var transaction = new Transaction(100, ACQUIRER, CardBrand.Mastercard, INVALID_TRANSACTION_TYPE);

            Amounts.ComputeNetAmount(transaction);
        }
        #endregion

        #region Compute tests
        [TestMethod]
        public void Compute()
        {
            double amount = 1000,
            netAssert = 973.5,
            net;

            net = Amounts.ComputeNetAmount(amount, "B", CardBrand.Mastercard, TransactionType.Credit);

            Assert.AreEqual(netAssert, net);
        }

        [TestMethod]
        public void Compute_Transaction()
        {
            var transaction = new Transaction(1000, "C", CardBrand.Visa, TransactionType.Debit);
            double netAssert = 978.4,
            net;

            net = Amounts.ComputeNetAmount(transaction);

            Assert.AreEqual(netAssert, net);
        }
        #endregion
    }
}
