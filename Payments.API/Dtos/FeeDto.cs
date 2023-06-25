using Payments.API.Dtos.Enums;

namespace Payments.API.Dtos;
public record class FeeDto(
    CardBrand CardBrand,
    double CreditFee,
    double DebitFee);