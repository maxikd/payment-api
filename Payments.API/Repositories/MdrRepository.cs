using System;
using System.Collections.Generic;
using System.Linq;
using Payments.API.Entities;
using Payments.API.Entities.Enums;
using Payments.API.Repositories.Abstractions;

namespace Payments.API.Repositories;
public class MdrRepository : IMdrRepository
{
    private readonly IEnumerable<Mdr> _fees;

    public MdrRepository()
    {
        _fees = InitializeFees();
    }

    public IEnumerable<Mdr> GetAll()
    {
        return _fees;
    }

    public double GetFee(
        string acquirer,
        CardBrand brand,
        TransactionType type)
    {
        var mdr = _fees.SingleOrDefault(mdr => mdr.Acquirer.Equals(acquirer, StringComparison.InvariantCultureIgnoreCase));
        if (mdr is null)
            throw new ArgumentOutOfRangeException(nameof(acquirer), acquirer, "Invalid Acquirer.");

        var fee = mdr.Fees.SingleOrDefault(fee => fee.CardBrand == brand);
        if (fee is null)
            throw new ArgumentOutOfRangeException(nameof(brand), brand, "Invalid Card Brand.");

        return type switch
        {
            TransactionType.Credit => fee.CreditFee,
            TransactionType.Debit => fee.DebitFee,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid Transaction Type.")
        };
    }

    private static IEnumerable<Mdr> InitializeFees()
    {
        var mdrs = new List<Mdr>();
        Mdr mdr;

        mdr = new Mdr
        {
            Acquirer = "A",
            Fees = new List<Fee>
            {
                new Fee(CardBrand.Visa, 2.25, 2.00),
                new Fee(CardBrand.Mastercard, 2.35, 1.98)
            }
        };
        mdrs.Add(mdr);

        mdr = new Mdr
        {
            Acquirer = "B",
            Fees = new List<Fee>
            {
                new Fee(CardBrand.Visa, 2.50, 2.08),
                new Fee(CardBrand.Mastercard, 2.65, 1.75)
            }
        };
        mdrs.Add(mdr);

        mdr = new Mdr
        {
            Acquirer = "C",
            Fees = new List<Fee>{
                new Fee(CardBrand.Visa, 2.75, 2.16),
                new Fee(CardBrand.Mastercard, 3.10, 1.58)
            }
        };
        mdrs.Add(mdr);

        return mdrs;
    }
}