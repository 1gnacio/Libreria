using Comun;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Negocios.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoAPI.Controllers
{
    [Route("api/[controller]")]
    public class LibroController : Controller
    {
        private readonly ILibroRepositorio _libroRepositorio;

        public LibroController(ILibroRepositorio libroRepositorio)
        {
            _libroRepositorio = libroRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetLibros()
        {
            var allLibros = await _libroRepositorio.ObtenerListaLibros();
            return Ok(allLibros);
        }

        [HttpGet("{libroId}")]
        public async Task<IActionResult> GetLibro(int? libroId)
        {
            if(libroId == null)
            {
                return BadRequest(new ErrorModel
                {
                    Titulo = "",
                    ErrorMensaje = "Libro Id Invalido",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var libroDetalles = await _libroRepositorio.ObtenerLibro(libroId.Value);

            if (libroDetalles == null)
            {
                return BadRequest(new ErrorModel
                {
                    Titulo = "",
                    ErrorMensaje = "Libro Id Invalido",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(libroDetalles);
        }
    }
}
