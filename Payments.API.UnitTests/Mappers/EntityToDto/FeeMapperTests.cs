using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Entities;
using Payments.API.Mappers.EntityToDto;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.EntityToDto;

public class FeeMapperTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(FeeMapper).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldMapAsExpected(
        Fee fee,
        Dtos.Enums.CardBrand cardBrand,
        FeeMapper sut)
    {
        sut.CardBrandMapper
            .Map(fee.CardBrand)
            .Returns(cardBrand);

        var result = sut.Map(fee);

        result.CardBrand.Should().Be(cardBrand);
        result.CreditFee.Should().Be(fee.CreditFee);
        result.DebitFee.Should().Be(fee.DebitFee);
    }
}