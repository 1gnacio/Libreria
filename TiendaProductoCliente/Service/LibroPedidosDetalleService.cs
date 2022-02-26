using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public Task<LibroPedidosDetalleDto> MarcarPagoExitoso(LibroPedidosDetalleDto detalles)
        {
            throw new NotImplementedException();
        }

        public Task<LibroPedidosDetalleDto> SaveLibroPedidosDetalle(LibroPedidosDetalleDto detalles)
        {
            throw new NotImplementedException();
        }
    }
}
