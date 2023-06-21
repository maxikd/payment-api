using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace Payments.API
{
    public class Program
    {
        public static async Task Main(
            string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            await app.RunAsync();
        }
    }
}