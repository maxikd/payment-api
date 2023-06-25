using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Payments.API.Contracts;
using Payments.API.Dtos;
using Payments.API.Entities;
using Payments.API.Mappers;
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

            builder.Services.AddControllers();

            builder.Services.AddSingleton<IMdrRepository, MdrRepository>();

            builder.Services.AddScoped<IMdrService, MdrService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            builder.Services.AddScoped<IMapper<Contracts.Enums.CardBrand, Dtos.Enums.CardBrand>, Mappers.ContractToDto.CardBrandMapper>();
            builder.Services.AddScoped<IMapper<Dtos.Enums.CardBrand, Entities.Enums.CardBrand>, Mappers.DtoToEntity.CardBrandMapper>();
            builder.Services.AddScoped<IMapper<Contracts.Enums.TransactionType, Dtos.Enums.TransactionType>, Mappers.ContractToDto.TransactionTypeMapper>();
            builder.Services.AddScoped<IMapper<Dtos.Enums.TransactionType, Entities.Enums.TransactionType>, Mappers.DtoToEntity.TransactionTypeMapper>();
            builder.Services.AddScoped<IMapper<Fee, FeeDto>, Mappers.EntityToDto.FeeMapper>();
            builder.Services.AddScoped<IMapper<FeeDto, PaymentFee>, Mappers.DtoToContract.FeeMapper>();
            builder.Services.AddScoped<IMapper<Mdr, MdrDto>, Mappers.EntityToDto.MdrMapper>();
            builder.Services.AddScoped<IMapper<MdrDto, PaymentMdr>, Mappers.DtoToContract.MdrMapper>();
            builder.Services.AddScoped<IMapper<double, PaymentNetAmount>, Mappers.DtoToContract.NetAmountMapper>();

            var app = builder.Build();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}