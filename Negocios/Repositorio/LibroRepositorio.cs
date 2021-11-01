using AccesoDatos.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Negocios.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Repositorio
{
    public class LibroRepositorio : ILibroRepositorio
    {
        private readonly AplicacionDbContext _db;
        private readonly IMapper _mapper;

        public LibroRepositorio(AplicacionDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LibroDto> ActualizarLibro(int libroId, LibroDto libroDto)
        {
            try
            {
                if(libroId == libroDto.Id)
                {
                    Libro libroDetalles = await _db.Libro.FindAsync(libroId);
                    Libro libro = _mapper.Map<LibroDto, Libro>(libroDto, libroDetalles);
                    libro.FechaActualizacion = DateTime.Now;
                    var updateLibro = _db.Libro.Update(libro);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Libro, LibroDto>(updateLibro.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<LibroDto> AgregarLibro(LibroDto libroDto)
        {
            Libro libro = _mapper.Map<LibroDto, Libro>(libroDto);
            libro.FechaCreacion = DateTime.Now;
            var addLibro = await _db.Libro.AddAsync(libro);
            await _db.SaveChangesAsync();
            return _mapper.Map<Libro, LibroDto>(addLibro.Entity);
        }

        public async Task<int> EliminarLibro(int libroId)
        {
            var libroDetalles = await _db.Libro.FindAsync(libroId);
            if(libroDetalles != null)
            {
                var imagenes = await _db.LibroImagen.Where(x => x.LibroId == libroId).ToListAsync();
                
                _db.LibroImagen.RemoveRange(imagenes);
                _db.Remove(libroDetalles);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<LibroDto> ExisteNombreLibro(string nombre, int libroId = 0)
        {
            try
            {
                if(libroId == 0)
                {
                    LibroDto libroDto = _mapper.Map<Libro, LibroDto>(await _db.Libro.FirstOrDefaultAsync(x => x.Nombre.ToLower() == nombre.ToLower()));
                    return libroDto;
                }
                else
                {
                    LibroDto libroDto = _mapper.Map<Libro, LibroDto>(await _db.Libro.FirstOrDefaultAsync(x => x.Nombre.ToLower() == nombre.ToLower() && x.Id != libroId));
                    return libroDto;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<LibroDto> ObtenerLibro(int libroId)
        {
            try
            {
                LibroDto libroDto = _mapper.Map<Libro, LibroDto>(await _db.Libro.Include(x => x.LibroImagen).FirstOrDefaultAsync(x => x.Id == libroId));
                return libroDto;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<LibroDto>> ObtenerListaLibros()
        {
            try
            {
                IEnumerable<LibroDto> libroDtos = _mapper.Map<IEnumerable<Libro>, IEnumerable<LibroDto>>(await _db.Libro.Include(x => x.LibroImagen).ToListAsync());
                return libroDtos;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
