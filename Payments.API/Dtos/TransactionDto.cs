using Payments.API.Dtos.Enums;

namespace Payments.API.Dtos;

public record class TransactionDto(
    double Amount,
    string Acquirer,
    CardBrand CardBrand,
    TransactionType TransactionType);