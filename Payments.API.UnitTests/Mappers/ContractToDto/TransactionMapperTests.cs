using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Contracts;
using Payments.API.Mappers.ContractToDto;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.ContractToDto;

public class TransactionMapperTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(TransactionMapper).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldMapAsExpected(
        PaymentTransaction transaction,
        Dtos.Enums.CardBrand cardBrand,
        Dtos.Enums.TransactionType transactionType,
        TransactionMapper sut)
    {
        sut.CardBrandMapper
            .Map(transaction.CardBrand)
            .Returns(cardBrand);
        sut.TransactionTypeMapper
            .Map(transaction.Type)
            .Returns(transactionType);

        var result = sut.Map(transaction);

        result.Acquirer.Should().Be(transaction.Acquirer);
        result.Amount.Should().Be(transaction.Amount);
        result.CardBrand.Should().Be(cardBrand);
        result.TransactionType.Should().Be(transactionType);
    }
}