using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoCliente.Service.IService
{
    public interface ILibroPedidosDetalleService
    {
        public Task<LibroPedidosDetalleDto> SaveLibroPedidosDetalle(LibroPedidosDetalleDto detalles);
        public Task<LibroPedidosDetalleDto> MarcarPagoExitoso(LibroPedidosDetalleDto detalles);

    }
}
