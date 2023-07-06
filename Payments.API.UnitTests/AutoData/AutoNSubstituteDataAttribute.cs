using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Payments.API.UnitTests.AutoData.Customizations;

namespace Payments.API.UnitTests.AutoData;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute() : base(
        () => new Fixture()
            .Customize(new AutoNSubstituteCustomization())
            .Customize(new ControllerCustomization()))
    {
    }
}