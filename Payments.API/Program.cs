using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Payments.API.Contracts;
using Payments.API.Entities;
using Payments.API.Mappers;
using Payments.API.Mappers.Requests;
using Payments.API.Mappers.Responses;
using Payments.API.Repositories;
using Payments.API.Repositories.Abstractions;
using Payments.API.Services;
using Payments.API.Services.Abstractions;
using System.Threading.Tasks;

namespace Payments.API
{
    public class Program
    {
        public static async Task Main(
            string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IMdrRepository, MdrRepository>();

            builder.Services.AddScoped<IMdrService, MdrService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            builder.Services.AddScoped<IMapper<Contracts.Enums.CardBrand, Entities.Enums.CardBrand>, CardBrandMapper>();
            builder.Services.AddScoped<IMapper<Contracts.Enums.TransactionType, Entities.Enums.TransactionType>, TransactionTypeMapper>();
            builder.Services.AddScoped<IMapper<Fee, PaymentFee>, FeeMapper>();
            builder.Services.AddScoped<IMapper<Mdr, PaymentMdr>, MdrMapper>();
            builder.Services.AddScoped<IMapper<double, PaymentNetAmount>, NetAmountMapper>();

            var app = builder.Build();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}