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
        IEnumerable<FeeDto> dtos,
        PaymentFee contract,
        FeesMapper sut)
    {
        sut.FeeMapper
            .Map(Arg.Any<FeeDto>())
            .Returns(contract);

        var result = sut.Map(dtos);

        result.Should().AllBeOfType<PaymentFee>();
        result.Should().HaveCount(3);
    }
}