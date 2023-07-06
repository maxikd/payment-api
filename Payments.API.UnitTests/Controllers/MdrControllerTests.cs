using System.Collections.Generic;
using System.Linq;
using AutoFixture.Idioms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Payments.API.Contracts;
using Payments.API.Controllers;
using Payments.API.Dtos;
using Payments.API.UnitTests.AutoData;
using Xunit;

namespace Payments.API.UnitTests.Controllers;

public class MdrControllerTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(MdrController).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void GetAll_ShouldReturnEnumerable(
        IEnumerable<MdrDto> dtos,
        PaymentMdr contract,
        MdrController sut)
    {
        sut.MdrService
            .GetAll()
            .Returns(dtos);
        sut.MdrMapper
            .Map(Arg.Any<MdrDto>())
            .Returns(contract);

        var result = sut.GetAll();

        var okResult = result.As<OkObjectResult>();

        var contracts = okResult.Value.As<IEnumerable<PaymentMdr>>();
        contracts.Should().HaveCount(3);
        contracts.All(c => c == contract).Should().BeTrue();
    }
}