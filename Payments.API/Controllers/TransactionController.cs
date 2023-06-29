using Microsoft.AspNetCore.Mvc;
using Payments.API.Contracts;
using Payments.API.Dtos;
using Payments.API.Mappers;
using Payments.API.Services.Abstractions;
using System;

namespace Payments.API.Controllers;

[ApiController]
[Route("api/payments")]
public class TransactionController : ControllerBase
{
    public TransactionController(
        ITransactionService transactionService,
        IMapper<PaymentTransaction, TransactionDto> transactionMapper,
        IMapper<double, PaymentNetAmount> netAmountMapper)
    {
        TransactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        TransactionMapper = transactionMapper ?? throw new ArgumentNullException(nameof(transactionMapper));
        NetAmountMapper = netAmountMapper ?? throw new ArgumentNullException(nameof(netAmountMapper));
    }

    public ITransactionService TransactionService { get; }
    public IMapper<PaymentTransaction, TransactionDto> TransactionMapper { get; }
    public IMapper<double, PaymentNetAmount> NetAmountMapper { get; }

    [HttpPost]
    [Route("transaction")]
    public IActionResult Execute(
        [FromBody] PaymentTransaction paymentTransaction)
    {
        try
        {
            var transaction = TransactionMapper.Map(paymentTransaction);

            var net = TransactionService.Execute(transaction);

            var netAmount = NetAmountMapper.Map(net);

            return Ok(netAmount);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
        catch (ArgumentOutOfRangeException e)
        {
            return BadRequest(e.Message);
        }
        catch (IndexOutOfRangeException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}