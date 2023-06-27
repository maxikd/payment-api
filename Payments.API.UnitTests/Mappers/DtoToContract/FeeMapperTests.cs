using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Dtos;
using Payments.API.Mappers.DtoToContract;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.DtoToContract;

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
        Contracts.Enums.CardBrand cardBrand,
        FeeDto dto,
        FeeMapper sut)
    {
        sut.CardBrandMapper
            .Map(dto.CardBrand)
            .Returns(cardBrand);

        var result = sut.Map(dto);

        result.CardBrand.Should().Be(cardBrand);
        result.Credit.Should().Be(dto.CreditFee);
        result.Debit.Should().Be(dto.DebitFee);
    }
}