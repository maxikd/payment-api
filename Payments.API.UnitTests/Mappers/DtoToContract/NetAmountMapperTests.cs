using FluentAssertions;
using Payments.API.Mappers.DtoToContract;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Mappers.DtoToContract;

public class NetAmountMapperTests
{
    [Theory, AutoNSubstituteData]
    public void Map_ShouldMapAsExpected(
        double value,
        NetAmountMapper sut)
    {
        var result = sut.Map(value);

        result.Net.Should().Be(value);
    }
}