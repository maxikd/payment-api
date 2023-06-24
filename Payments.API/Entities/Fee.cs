using Payments.API.Entities.Enums;

namespace Payments.API.Entities;

public record Fee(CardBrand CardBrand, double CreditFee, double DebitFee);