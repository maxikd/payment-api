using System.Collections.Generic;
using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Dtos;
using Payments.API.Entities;
using Payments.API.Mappers.EntityToDto;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.EntityToDto;

public class MdrMapperTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(MdrMapper).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void Map_ShouldMapAsExpected(
        Mdr mdr,
        IEnumerable<FeeDto> feeDtos,
        MdrMapper sut)
    {
        sut.FeeMapper
            .Map(mdr.Fees)
            .Returns(feeDtos);

        var result = sut.Map(mdr);

        result.Acquirer.Should().Be(mdr.Acquirer);
        result.Fees.Should().BeEquivalentTo(feeDtos);
    }
}