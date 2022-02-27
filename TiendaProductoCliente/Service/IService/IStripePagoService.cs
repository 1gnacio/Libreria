using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoCliente.Service.IService
{
    public interface IStripePagoService
    {
        public Task<SuccessModel> Checkout(StripePagoDto modelo);
    }
}
