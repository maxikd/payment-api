using System;
using FluentAssertions;
using Payments.API.Mappers.DtoToEntity;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.DtoToEntity;

public class CardBrandMapperTests
{
    [Theory]
    [InlineAutoNSubstituteData(Dtos.Enums.CardBrand.Mastercard, Entities.Enums.CardBrand.Mastercard)]
    [InlineAutoNSubstituteData(Dtos.Enums.CardBrand.Visa, Entities.Enums.CardBrand.Visa)]
    public void Map_ShouldMapToCorrectEnum(
        Dtos.Enums.CardBrand cardBrand,
        Entities.Enums.CardBrand expectedCardBrand,
        CardBrandMapper sut)
    {
        var result = sut.Map(cardBrand);

        result.Should().Be(expectedCardBrand);
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldThrowArgumentOutOfRangeException(
        CardBrandMapper sut)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => sut.Map(0));
    }
}