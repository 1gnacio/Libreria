using AccesoDatos.Data;
using AutoMapper;
using Comun;
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
    public class LibroPedidosDetalleRepositorio : ILibroPedidosDetalleRepositorio
    {
        private readonly AplicacionDbContext _db;
        private readonly IMapper _mapper;

        public LibroPedidosDetalleRepositorio(AplicacionDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task<bool> ActualizarPedidoStatus(int libroPedidoId, string status)
        {
            throw new NotImplementedException();
        }

        public async Task<LibroPedidosDetalleDto> Crear(LibroPedidosDetalleDto detalles)
        {
            try
            {
                var libroPedido = _mapper.Map<LibroPedidosDetalleDto, LibroPedidosDetalle>(detalles);
                libroPedido.Status = SD.Status_Pendiente;
                var resultado = await _db.LibroPedidosDetalle.AddAsync(libroPedido);
                await _db.SaveChangesAsync();
                return _mapper.Map<LibroPedidosDetalle, LibroPedidosDetalleDto>(resultado.Entity);
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<LibroPedidosDetalleDto> GetLibroPedidoDetalle(int libroPedidoId)
        {
            try
            {
                LibroPedidosDetalle libroPedido =
                    await _db.LibroPedidosDetalle
                    .Include(u => u.Libro)
                    .ThenInclude(x => x.LibroImagen)
                    .FirstOrDefaultAsync(x => x.Id == libroPedidoId);

                LibroPedidosDetalleDto libroPedidosDetalleDto = _mapper.Map<LibroPedidosDetalle, LibroPedidosDetalleDto>(libroPedido);

                return libroPedidosDetalleDto;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<LibroPedidosDetalleDto>> GetListaLibroPedidosDetalle()
        {
            try
            {
                IEnumerable<LibroPedidosDetalleDto> libroPedidos = 
                    _mapper.Map<IEnumerable<LibroPedidosDetalle>, IEnumerable<LibroPedidosDetalleDto>>
                    (_db.LibroPedidosDetalle.Include(u => u.Libro));
                return libroPedidos;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public Task<LibroPedidosDetalleDto> MarcarPagoExitoso(int id)
        {
            throw new NotImplementedException();
        }
    }
}
