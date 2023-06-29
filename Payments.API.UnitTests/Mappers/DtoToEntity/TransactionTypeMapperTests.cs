using System;
using FluentAssertions;
using Payments.API.Mappers.DtoToEntity;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.DtoToEntity;

public class TransactionTypeMapperTests
{
    [Theory]
    [InlineAutoNSubstituteData(Dtos.Enums.TransactionType.Credit, Entities.Enums.TransactionType.Credit)]
    [InlineAutoNSubstituteData(Dtos.Enums.TransactionType.Debit, Entities.Enums.TransactionType.Debit)]
    public void Map_ShouldMapToCorrectEnum(
        Dtos.Enums.TransactionType transactionType,
        Entities.Enums.TransactionType expectedTransactionType,
        TransactionTypeMapper sut)
    {
        var result = sut.Map(transactionType);

        result.Should().Be(expectedTransactionType);
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldThrowArgumentOutOfRangeException(
        TransactionTypeMapper sut)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.Map(0));
    }
}