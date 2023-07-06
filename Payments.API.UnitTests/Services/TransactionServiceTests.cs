using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Dtos;
using Payments.API.Services;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Services;

public class TransactionServiceTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(TransactionService).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void Execute_ShouldReturnExpectedAmount(
        TransactionDto transactionDto,
        Entities.Enums.CardBrand cardBrand,
        Entities.Enums.TransactionType transactionType,
        double percentage,
        TransactionService sut)
    {
        var fee = transactionDto.Amount * percentage / 100;
        var expectedNetAmount = transactionDto.Amount - fee;

        sut.CardBrandMapper
            .Map(transactionDto.CardBrand)
            .Returns(cardBrand);
        sut.TransactionTypeMapper
            .Map(transactionDto.TransactionType)
            .Returns(transactionType);
        sut.MdrRepository
            .GetFee(transactionDto.Acquirer, cardBrand, transactionType)
            .Returns(percentage);

        var result = sut.Execute(transactionDto);

        result.Should().Be(expectedNetAmount);
    }
}