using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using RentWeb2.Shared;
using BlazorDownloadFile;
using RentWeb2.Services;
using Blazored.Toast;
using Blazored.Modal;
using BlazorTable;


namespace RentWeb2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //builder.RootComponents.Add<App>("app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //await builder.Build().RunAsync();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddBlazoredLocalStorage()
                .AddBlazorTable()
                .AddBlazoredSessionStorage()
                .AddBlazorDownloadFile()
                .AddBlazoredModal()
                .AddSingleton<ConnectAPI>()
                .AddSingleton<Service>()
                .AddScoped<Service>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<ConnectAPI>().AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddBlazoredToast();
            //  builder.Services.AddCookies();

            


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var host = builder.Build();

            // читаем данные о авторизации при загрузке
            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            // читаем состояние редактирования при загрузке
            var service = host.Services.GetRequiredService<Service>();
            await service.Initialize();

            await host.RunAsync();
        }
    }
}
