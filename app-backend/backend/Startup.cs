using backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    // Parses requests & serializes responses using snake_case.
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                });

            services.AddDbContext<AccountingContext>(options =>
            {
                options
                    .UseInMemoryDatabase("accounting_db")
                    .ConfigureWarnings(warnings =>
                    {
                        // In-memory db does not support transactions and throws exceptions if warnings are not ignored.
                        warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                    });

            });

            services.AddScoped<IAccountingService, AccountingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}