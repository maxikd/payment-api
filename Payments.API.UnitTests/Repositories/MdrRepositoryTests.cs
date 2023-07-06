using System;
using FluentAssertions;
using Payments.API.Entities.Enums;
using Payments.API.Repositories;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Repositories;

public class MdrRepositoryTests
{
    [Theory, AutoNSubstituteData]
    public void GetAll_ShouldReturnAllMdrs(
        MdrRepository sut)
    {
        var mdrs = sut.GetAll();

        mdrs.Should().HaveCount(3);
    }

    [Theory, AutoNSubstituteData]
    public void GetFee_WhenInvalidAcquirer_ShouldThrowArgumentOutOfRangeException(
        string acquirer,
        CardBrand cardBrand,
        TransactionType transactionType,
        MdrRepository sut)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.GetFee(acquirer, cardBrand, transactionType));

        exception.Message.Should().StartWith("Invalid Acquirer.");
        exception.ParamName.Should().Be("acquirer");
    }

    [Theory, AutoNSubstituteData]
    public void GetFee_WhenNoFeeForCardBrand_ShouldThrowArgumentOutOfRangeException(
        TransactionType transactionType,
        MdrRepository sut)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.GetFee("A", 0, transactionType));

        exception.Message.Should().StartWith("Invalid Card Brand.");
        exception.ParamName.Should().Be("brand");
    }

    [Theory, AutoNSubstituteData]
    public void GetFee_WhenInvalidTransactionType_ShouldThrowArgumentOutOfRangeException(
        CardBrand cardBrand,
        MdrRepository sut)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.GetFee("A", cardBrand, 0));

        exception.Message.Should().StartWith("Invalid Transaction Type.");
        exception.ParamName.Should().Be("type");
    }

    [Theory]
    [InlineAutoNSubstituteData("A", CardBrand.Mastercard, TransactionType.Credit, 2.35)]
    [InlineAutoNSubstituteData("A", CardBrand.Mastercard, TransactionType.Debit, 1.98)]
    [InlineAutoNSubstituteData("A", CardBrand.Visa, TransactionType.Credit, 2.25)]
    [InlineAutoNSubstituteData("A", CardBrand.Visa, TransactionType.Debit, 2.00)]
    [InlineAutoNSubstituteData("B", CardBrand.Mastercard, TransactionType.Credit, 2.65)]
    [InlineAutoNSubstituteData("B", CardBrand.Mastercard, TransactionType.Debit, 1.75)]
    [InlineAutoNSubstituteData("B", CardBrand.Visa, TransactionType.Credit, 2.50)]
    [InlineAutoNSubstituteData("B", CardBrand.Visa, TransactionType.Debit, 2.08)]
    [InlineAutoNSubstituteData("C", CardBrand.Mastercard, TransactionType.Credit, 3.10)]
    [InlineAutoNSubstituteData("C", CardBrand.Mastercard, TransactionType.Debit, 1.58)]
    [InlineAutoNSubstituteData("C", CardBrand.Visa, TransactionType.Credit, 2.75)]
    [InlineAutoNSubstituteData("C", CardBrand.Visa, TransactionType.Debit, 2.16)]
    public void GetFee_ShouldReturnExpectedFee(
        string acquirer,
        CardBrand cardBrand,
        TransactionType transactionType,
        double expectedFee,
        MdrRepository sut)
    {
        var fee = sut.GetFee(acquirer, cardBrand, transactionType);

        fee.Should().Be(expectedFee);
    }
}