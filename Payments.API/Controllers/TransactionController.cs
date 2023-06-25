using Microsoft.AspNetCore.Mvc;
using Payments.API.Contracts;
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
        IMapper<Contracts.Enums.CardBrand, Dtos.Enums.CardBrand> cardBrandMapper,
        IMapper<Contracts.Enums.TransactionType, Dtos.Enums.TransactionType> transactionTypeMapper,
        IMapper<double, PaymentNetAmount> netAmountMapper)
    {
        TransactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        CardBrandMapper = cardBrandMapper ?? throw new ArgumentNullException(nameof(cardBrandMapper));
        TransactionTypeMapper = transactionTypeMapper ?? throw new ArgumentNullException(nameof(transactionTypeMapper));
        NetAmountMapper = netAmountMapper ?? throw new ArgumentNullException(nameof(netAmountMapper));
    }

    public ITransactionService TransactionService { get; }
    public IMapper<Contracts.Enums.CardBrand, Dtos.Enums.CardBrand> CardBrandMapper { get; }
    public IMapper<Contracts.Enums.TransactionType, Dtos.Enums.TransactionType> TransactionTypeMapper { get; }
    public IMapper<double, PaymentNetAmount> NetAmountMapper { get; }

    [HttpPost]
    [Route("transaction")]
    public IActionResult Execute(
        [FromBody] PaymentTransaction paymentTransaction)
    {
        try
        {
            var cardBrand = CardBrandMapper.Map(paymentTransaction.CardBrand);
            var transactionType = TransactionTypeMapper.Map(paymentTransaction.Type);

            var net = TransactionService.Execute(
                paymentTransaction.Amount,
                paymentTransaction.Acquirer,
                cardBrand,
                transactionType);

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