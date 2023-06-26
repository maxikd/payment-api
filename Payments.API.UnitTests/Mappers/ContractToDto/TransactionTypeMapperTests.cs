using System;
using FluentAssertions;
using Payments.API.Mappers.ContractToDto;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.ContractToDto;

public class TransactionTypeMapperTests
{
    [Theory]
    [InlineAutoNSubstituteData(Contracts.Enums.TransactionType.Credito, Dtos.Enums.TransactionType.Credit)]
    [InlineAutoNSubstituteData(Contracts.Enums.TransactionType.Debito, Dtos.Enums.TransactionType.Debit)]
    public void Map_ShouldMapToCorrectEnum(
        Contracts.Enums.TransactionType input,
        Dtos.Enums.TransactionType expected,
        TransactionTypeMapper sut)
    {
        var result = sut.Map(input);

        result.Should().Be(expected);
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldThrowArgumentOutOfRangeException(
        TransactionTypeMapper sut)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.Map(0));
    }
}