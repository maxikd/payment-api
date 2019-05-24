using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment_API.Enums;
using Payment_API.Model;
using System;

namespace PaymentAPITests.Model
{
    [TestClass]
    public class MDRDataTests
    {
        private const string ACQUIRER = "A";
        private const string INVALID_ACQUIRER = "ABC";
        private const string INVALID_CARD_BRAND = "APPLE";
        private const string INVALID_TRANSACTION_TYPE = "PHONE";

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MDRData.GetMDR();
        }

        #region Exception tests
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void GetFee_Acquirer_Null_ThrowsArgumentNullException()
        {
            MDRData.GetFee(null, CardBrand.Visa, TransactionType.Debit);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void GetFee_Acquirer_Empty_ThrowsArgumentNullException()
        {
            MDRData.GetFee(string.Empty, CardBrand.Visa, TransactionType.Debit);
        }
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetFee_Acquirer_Empty_ThrowsArgumentOutOfRangeException()
        {
            MDRData.GetFee(INVALID_ACQUIRER, CardBrand.Visa, TransactionType.Debit);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void GetFee_CardBrand_Null_ThrowsArgumentNullException()
        {
            MDRData.GetFee(ACQUIRER, null, TransactionType.Debit);
        }
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void GetFee_CardBrand_Invalid_ThrowsArgumentNullException()
        {
            MDRData.GetFee(ACQUIRER, INVALID_CARD_BRAND, TransactionType.Debit);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void GetFee_TransactionType_Null_ThrowsArgumentNullException()
        {
            MDRData.GetFee(ACQUIRER, CardBrand.Visa, null);
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetFee_TransactionType_Invalid_ThrowsIndexOutOfRangeException()
        {
            MDRData.GetFee(ACQUIRER, CardBrand.Visa, INVALID_TRANSACTION_TYPE);
        }
        #endregion

        #region Fee tests
        [TestMethod]
        public void GetFee_Visa_Credit()
        {
            double feeAssert = 2.25,
                fee;

            fee = MDRData.GetFee("A", CardBrand.Visa, TransactionType.Credit);

            Assert.AreEqual(feeAssert, fee);
        }
        [TestMethod]
        public void GetFee_Visa_Debit()
        {
            double feeAssert = 2.08,
                fee;

            fee = MDRData.GetFee("B", CardBrand.Visa, TransactionType.Debit);

            Assert.AreEqual(feeAssert, fee);
        }
        [TestMethod]
        public void GetFee_Mastercard_Credit()
        {
            double feeAssert = 3.10,
                fee;

            fee = MDRData.GetFee("C", CardBrand.Mastercard, TransactionType.Credit);

            Assert.AreEqual(feeAssert, fee);
        }
        [TestMethod]
        public void GetFee_Mastercard_Debit()
        {
            double feeAssert = 1.75,
                fee;

            fee = MDRData.GetFee("B", CardBrand.Mastercard, TransactionType.Debit);

            Assert.AreEqual(feeAssert, fee);
        }
        #endregion
    }
}
