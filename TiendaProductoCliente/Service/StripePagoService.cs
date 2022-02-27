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
    public class StripePagoService : IStripePagoService
    {
        private readonly HttpClient _client;

        public StripePagoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SuccessModel> Checkout(StripePagoDto modelo)
        {
            var contenido = JsonConvert.SerializeObject(modelo);

            var bodyContenido = new StringContent(contenido, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/stripepago/crear", bodyContenido);

            if (response.IsSuccessStatusCode)
            {
                var contenidoTemp = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<SuccessModel>(contenidoTemp);

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
