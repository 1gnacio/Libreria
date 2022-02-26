using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Repositorio.IRepositorio
{
    public interface ILibroRepositorio
    {
        public Task<LibroDto> AgregarLibro(LibroDto libroDto);

        public Task<LibroDto> ActualizarLibro(int libroId, LibroDto libroDto);

        public Task<LibroDto> ObtenerLibro(int libroId);

        public Task<int> EliminarLibro(int libroId);

        public Task<IEnumerable<LibroDto>> ObtenerListaLibros(string autor = null);

        public Task<LibroDto> ExisteNombreLibro(string nombre, int libroId = 0);

    }
}
