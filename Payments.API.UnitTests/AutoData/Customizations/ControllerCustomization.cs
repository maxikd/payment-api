using AutoFixture;

namespace Payments.API.UnitTests.AutoData.Customizations;

public class ControllerCustomization : ICustomization
{
    public void Customize(
        IFixture fixture)
    {
        fixture.OmitAutoProperties = true;
    }
}