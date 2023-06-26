using AutoFixture.Xunit2;

namespace Payments.API.UnitTests.AutoData;

public class InlineAutoNSubstituteDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(
        params object[] values) : base(
            new AutoNSubstituteDataAttribute(),
            values)
    {
    }
}