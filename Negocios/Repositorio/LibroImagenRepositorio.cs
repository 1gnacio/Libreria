using AccesoDatos.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Negocios.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Repositorio
{
    public class LibroImagenRepositorio : ILibroImagenRepositorio
    {
        private readonly AplicacionDbContext _db;
        private readonly IMapper _mapper;

        public LibroImagenRepositorio(AplicacionDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> AgregarLibroImagen(LibroImagenDto imagenDto)
        {
            var imagen = _mapper.Map<LibroImagenDto, LibroImagen>(imagenDto);
            await _db.LibroImagen.AddAsync(imagen);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> EliminarLibroImagenPorId(int imagenId)
        {
            var imagen = await _db.LibroImagen.FindAsync(imagenId);
            _db.LibroImagen.Remove(imagen);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> EliminarLibroImagenPorImagenUrl(string imagenUrl)
        {
            var imagenes = await _db.LibroImagen.FirstOrDefaultAsync(x => x.LibroImagenUrl.ToLower() == imagenUrl.ToLower());
            if (imagenes == null)
            {
                return 0;
            }
            
            _db.LibroImagen.Remove(imagenes);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> EliminarLibroImagenPorLibroId(int libroId)
        {
            var imagenLista = await _db.LibroImagen.Where(x => x.LibroId == libroId).ToListAsync();
            _db.LibroImagen.RemoveRange(imagenLista);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<LibroImagenDto>> ObtenerListaLibroImagen(int libroId)
        {
            return _mapper.Map<IEnumerable<LibroImagen>, IEnumerable<LibroImagenDto>>(
            await _db.LibroImagen.Where(x => x.LibroId == libroId).ToListAsync());
        }
    }
}
