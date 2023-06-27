using System.Collections.Generic;
using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Contracts;
using Payments.API.Dtos;
using Payments.API.Mappers.DtoToContract;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.DtoToContract;

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
        MdrDto dto,
        IEnumerable<PaymentFee> contracts,
        MdrMapper sut)
    {
        sut.FeesMapper
            .Map(dto.Fees)
            .Returns(contracts);

        var result = sut.Map(dto);

        result.Acquirer.Should().Be(dto.Acquirer);
        result.Fees.Should().BeEquivalentTo(contracts);
    }
}