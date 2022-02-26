using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Repositorio.IRepositorio
{
    public interface ILibroPedidosDetalleRepositorio
    {
        public Task<LibroPedidosDetalleDto> Crear(LibroPedidosDetalleDto detalles);
        public Task<LibroPedidosDetalleDto> MarcarPagoExitoso(int id);
        public Task<LibroPedidosDetalleDto> GetLibroPedidoDetalle(int libroPedidoId);
        public Task<IEnumerable<LibroPedidosDetalleDto>> GetListaLibroPedidosDetalle();
        public Task<bool> ActualizarPedidoStatus(int libroPedidoId, string status);

    }
}
