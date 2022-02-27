using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePagoController : Controller
    {
        private readonly IConfiguration _configuration;

        public StripePagoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(StripePagoDto pago)
        {
            try
            {
                var dominio = _configuration.GetValue<string>("TiendaProducto_Cliente_URL");
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = pago.Monto,
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = pago.ProductoNombre
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = dominio + "/success-payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = dominio + pago.ReturnUrl
                };

                var service = new SessionService();

                Session sesion = await service.CreateAsync(options);

                return Ok(new SuccessModel()
                {
                    Data = sesion.Id
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorModel() 
                { 
                    ErrorMensaje = e.Message
                });
            }
        }
    }
}
