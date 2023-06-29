using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Dtos;
using Payments.API.Entities;
using Payments.API.Mappers.EntityToDto;
using Payments.API.UnitTests.AutoData;
using System.Collections.Generic;
using Xunit;

namespace Payments.API.UnitTests.Mappers.EntityToDto;

public class FeesMapperTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(FeesMapper).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldMapAsExpected(
        IEnumerable<Fee> fees,
        FeeDto dto,
        FeesMapper sut)
    {
        sut.FeeMapper
            .Map(Arg.Any<Fee>())
            .Returns(dto);

        var result = sut.Map(fees);

        result.Should().AllBeOfType<FeeDto>();
        result.Should().HaveCount(3);
    }
}