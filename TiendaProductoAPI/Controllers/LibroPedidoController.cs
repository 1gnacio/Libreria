using Microsoft.AspNetCore.Mvc;
using Modelos;
using Negocios.Repositorio.IRepositorio;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LibroPedidoController : Controller
    {
        private readonly ILibroPedidosDetalleRepositorio _repositorio;

        public LibroPedidoController(ILibroPedidosDetalleRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] LibroPedidosDetalleDto detalles)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _repositorio.Crear(detalles);

                return Ok(resultado);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMensaje = "Error al crear Pedidos Detalle"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PagoExitoso([FromBody] LibroPedidosDetalleDto detalles)
        {
            var service = new SessionService();

            var sesionDetalles = service.Get(detalles.StripeSessionId);

            if (sesionDetalles.PaymentStatus == "paid")
            {
                var resultado = await _repositorio.MarcarPagoExitoso(detalles.Id);

                if (resultado == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMensaje = "No se puede marcar el pago como exitoso"
                    });
                }

                return Ok(resultado);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMensaje = "No se puede marcar el pago como exitoso"
                });
            }
            
        }
    }
}
