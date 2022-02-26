using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TiendaProductoCliente.Service.IService;

namespace TiendaProductoCliente.Service
{
    public class LibroService : ILibroService
    {
        private readonly HttpClient _client;

        public LibroService(HttpClient client)
        {
            _client = client;
        }

        public async Task<LibroDto> GetLibroDetalles(int libroId)
        {
            var response = await _client.GetAsync($"api/libro/{libroId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var libro = JsonConvert.DeserializeObject<LibroDto>(content);

                return libro;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);

                throw new Exception(errorModel.ErrorMensaje);
            }
        }

        public async Task<IEnumerable<LibroDto>> GetLibros(string autor)
        {
            var response = await _client.GetAsync($"api/libro?autor={autor}");
            var content = await response.Content.ReadAsStringAsync();
            var libros = JsonConvert.DeserializeObject<IEnumerable<LibroDto>>(content);

            return libros;
        }
    }
}
