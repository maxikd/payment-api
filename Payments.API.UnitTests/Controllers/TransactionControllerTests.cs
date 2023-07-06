using System;
using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Payments.API.Contracts;
using Payments.API.Controllers;
using Payments.API.Dtos;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Controllers;

public class TransactionControllerTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(TransactionController).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void Execute_ShouldExecuteAsExpected(
        double netAmount,
        PaymentTransaction paymentTransaction,
        TransactionDto transactionDto,
        PaymentNetAmount paymentNetAmount,
        TransactionController sut)
    {
        sut.TransactionMapper
            .Map(paymentTransaction)
            .Returns(transactionDto);
        sut.TransactionService
            .Execute(transactionDto)
            .Returns(netAmount);
        sut.NetAmountMapper
            .Map(netAmount)
            .Returns(paymentNetAmount);

        var result = sut.Execute(paymentTransaction);
        result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = result.As<OkObjectResult>();
        okObjectResult.StatusCode.Should().Be(200);
        okObjectResult.Value.Should().BeOfType<PaymentNetAmount>();
        okObjectResult.Value.Should().Be(paymentNetAmount);
    }

    [Theory, AutoNSubstituteData]
    public void Execute_WhenThrowsArgumentNullException_ShouldReturnBadRequest(
        PaymentTransaction paymentTransaction,
        TransactionDto transactionDto,
        TransactionController sut)
    {
        sut.TransactionMapper
            .Map(paymentTransaction)
            .Returns(transactionDto);
        sut.TransactionService
            .Execute(transactionDto)
            .Throws<ArgumentNullException>();

        var result = sut.Execute(paymentTransaction);
        result.Should().BeOfType<BadRequestObjectResult>();

        var badRequestObjectResult = result.As<BadRequestObjectResult>();
        badRequestObjectResult.StatusCode.Should().Be(400);
        badRequestObjectResult.Value.Should().Be("Value cannot be null.");
    }

    [Theory, AutoNSubstituteData]
    public void Execute_WhenThrowsArgumentOutOfRangeException_ShouldReturnBadRequest(
        PaymentTransaction paymentTransaction,
        TransactionDto transactionDto,
        TransactionController sut)
    {
        sut.TransactionMapper
            .Map(paymentTransaction)
            .Returns(transactionDto);
        sut.TransactionService
            .Execute(transactionDto)
            .Throws<ArgumentOutOfRangeException>();

        var result = sut.Execute(paymentTransaction);
        result.Should().BeOfType<BadRequestObjectResult>();

        var badRequestObjectResult = result.As<BadRequestObjectResult>();
        badRequestObjectResult.StatusCode.Should().Be(400);
        badRequestObjectResult.Value.Should().Be("Specified argument was out of the range of valid values.");
    }

    [Theory, AutoNSubstituteData]
    public void Execute_WhenThrowsIndexOutOfRangeException_ShouldReturnBadRequest(
        PaymentTransaction paymentTransaction,
        TransactionDto transactionDto,
        TransactionController sut)
    {
        sut.TransactionMapper
            .Map(paymentTransaction)
            .Returns(transactionDto);
        sut.TransactionService
            .Execute(transactionDto)
            .Throws<IndexOutOfRangeException>();

        var result = sut.Execute(paymentTransaction);
        result.Should().BeOfType<BadRequestObjectResult>();

        var badRequestObjectResult = result.As<BadRequestObjectResult>();
        badRequestObjectResult.StatusCode.Should().Be(400);
        badRequestObjectResult.Value.Should().Be("Index was outside the bounds of the array.");
    }

    [Theory, AutoNSubstituteData]
    public void Execute_WhenThrowsException_ShouldReturnInternalServerError(
        PaymentTransaction paymentTransaction,
        TransactionDto transactionDto,
        TransactionController sut)
    {
        sut.TransactionMapper
            .Map(paymentTransaction)
            .Returns(transactionDto);
        sut.TransactionService
            .Execute(transactionDto)
            .Throws<Exception>();

        var result = sut.Execute(paymentTransaction);
        result.Should().BeOfType<ObjectResult>();

        var badRequestObjectResult = result.As<ObjectResult>();
        badRequestObjectResult.StatusCode.Should().Be(500);
        badRequestObjectResult.Value.Should().Be("An unexpected error occured.");
    }
}