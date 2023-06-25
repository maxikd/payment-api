using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Payments.API.Contracts;
using Payments.API.Dtos;
using Payments.API.Mappers;
using Payments.API.Services.Abstractions;

namespace Payments.API.Controllers;

[ApiController]
[Route("api/payments")]
public class MdrController : ControllerBase
{
    public MdrController(
        IMdrService mdrService,
        IMapper<MdrDto, PaymentMdr> mdrMapper)
    {
        MdrService = mdrService ?? throw new ArgumentNullException(nameof(mdrService));
        MdrMapper = mdrMapper ?? throw new ArgumentNullException(nameof(mdrMapper));
    }

    public IMdrService MdrService { get; }
    public IMapper<MdrDto, PaymentMdr> MdrMapper { get; }

    [HttpGet]
    [Route("mdr")]
    public IActionResult GetAll()
    {
        var mdrs = MdrService.GetAll();
        var paymentMdrs = mdrs.Select(entity => MdrMapper.Map(entity));

        return Ok(paymentMdrs);
    }
}