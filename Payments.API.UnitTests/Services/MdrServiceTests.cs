using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using Payments.API.Dtos;
using Payments.API.Entities;
using Payments.API.Services;
using Payments.API.UnitTests.AutoData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Payments.API.UnitTests.Services;

public class MdrServiceTests
{
    [Theory, AutoNSubstituteData]
    public void GuardTests(
        GuardClauseAssertion assertion)
    {
        assertion.Verify(typeof(MdrService).GetConstructors());
    }

    [Theory, AutoNSubstituteData]
    public void GetAll_ShouldReturnAllMdrs(
        IEnumerable<Mdr> mdrs,
        MdrDto dto,
        MdrService sut)
    {
        sut.MdrRepository
            .GetAll()
            .Returns(mdrs);
        sut.DtoMapper
            .Map(Arg.Any<Mdr>())
            .Returns(dto);

        var result = sut.GetAll();

        result.Should().HaveCount(3);
        result.Should().AllBeOfType<MdrDto>();
        result.All(obj => obj == dto).Should().BeTrue();
    }
}