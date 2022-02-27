using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class StripePagoDto
    {
        public string ProductoNombre { get; set; }
        public long Monto { get; set; }
        public string ImagenUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
