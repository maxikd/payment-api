using System;
using FluentAssertions;
using Payments.API.Mappers.ContractToDto;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.ContractToDto;

public class CardBrandMapperTests
{
    [Theory]
    [InlineAutoNSubstituteData(Contracts.Enums.CardBrand.Master, Dtos.Enums.CardBrand.Mastercard)]
    [InlineAutoNSubstituteData(Contracts.Enums.CardBrand.Visa, Dtos.Enums.CardBrand.Visa)]
    public void Map_ShouldMapToCorrectEnum(
        Contracts.Enums.CardBrand input,
        Dtos.Enums.CardBrand expected,
        CardBrandMapper sut)
    {
        var result = sut.Map(input);

        result.Should().Be(expected);
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldThrowArgumentOutOfRangeException(
        CardBrandMapper sut)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.Map(0));
    }
}