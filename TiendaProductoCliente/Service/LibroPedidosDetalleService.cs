using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TiendaProductoCliente.Service.IService;

namespace TiendaProductoCliente.Service
{
    public class LibroPedidosDetalleService : ILibroPedidosDetalleService
    {
        private readonly HttpClient _client;

        public LibroPedidosDetalleService(HttpClient client)
        {
            _client = client;
        }

        public async Task<LibroPedidosDetalleDto> MarcarPagoExitoso(LibroPedidosDetalleDto detalles)
        {
            var contenido = JsonConvert.SerializeObject(detalles);

            var bodyContenido = new StringContent(contenido, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/libropedido/pagoexitoso", bodyContenido);

            if (response.IsSuccessStatusCode)
            {
                var contenidoTemp = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroPedidosDetalleDto>(contenidoTemp);

                return resultado;
            }
            else
            {
                var contenidoTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contenidoTemp);

                throw new Exception(errorModel.ErrorMensaje);
            }
        }

        public async Task<LibroPedidosDetalleDto> SaveLibroPedidosDetalle(LibroPedidosDetalleDto detalles)
        {
            var contenido = JsonConvert.SerializeObject(detalles);

            var bodyContenido = new StringContent(contenido, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/libropedido/crear", bodyContenido);

            if (response.IsSuccessStatusCode)
            {
                var contenidoTemp = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LibroPedidosDetalleDto>(contenidoTemp);

                return resultado;
            }
            else
            {
                var contenidoTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contenidoTemp);

                throw new Exception(errorModel.ErrorMensaje);
            }
        }
    }
}
