using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TiendaProductoCliente.Service;
using TiendaProductoCliente.Service.IService;

namespace TiendaProductoCliente
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ILibroService, LibroService>();
            builder.Services.AddScoped<ILibroPedidosDetalleService, LibroPedidosDetalleService>();
            builder.Services.AddScoped<IStripePagoService, StripePagoService>();
            await builder.Build().RunAsync();
        }
    }
}
