using Controllers;
using TimerTriggers;
using Interfaces;
using Services;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Functions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Repositories;
using Context;

namespace BuyMyHouse
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureOpenApi()
                .ConfigureServices(Configure)
                .Build();

            host.Run();
        }

        static void Configure(HostBuilderContext Builder, IServiceCollection Services)
        {
            Services.AddSingleton<IOpenApiHttpTriggerContext, OpenApiHttpTriggerContext>();
            Services.AddSingleton<IOpenApiTriggerFunction, OpenApiTriggerFunction>();

            Services.AddSingleton<UserController>();
            Services.AddSingleton<IUserService, UserService>();
            Services.AddTransient<IUserRepository, UserRepository>();
            
            Services.AddSingleton<MortgageTimerTrigger>();
            Services.AddSingleton<EmailTimerTrigger>();

            Services.AddSingleton<MortgageController>();
            Services.AddSingleton<IMortgageService, MortgageService>();
            Services.AddTransient<IMortgageRepository, MortgageRepository>();

            Services.AddSingleton<IEmailService, EmailService>();

            Services.AddTransient<CosmosDBContext>();
        }
    }
}