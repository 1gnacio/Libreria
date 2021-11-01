using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Repositorio.IRepositorio
{
    public interface ILibroImagenRepositorio
    {
        public Task<int> AgregarLibroImagen(LibroImagenDto imagenDto);
        public Task<int> EliminarLibroImagenPorId(int imagenId);
        public Task<int> EliminarLibroImagenPorLibroId(int libroId);
        public Task<int> EliminarLibroImagenPorImagenUrl(string imagenUrl);
        public Task<IEnumerable<LibroImagenDto>> ObtenerListaLibroImagen(int libroId);


    }
}
