using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoCliente.Service.IService
{
    public interface ILibroService
    {
        public Task<IEnumerable<LibroDto>> GetLibros(string autor);
        public Task<LibroDto> GetLibroDetalles(int libroId);
    }
}
