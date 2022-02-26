using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class LibroPedidosDetalleDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string StripeSessionId { get; set; }

        [Required]
        public long CostoTotal { get; set; }

        [Required]
        public int LibroId { get; set; }

        public bool PagoRealizado { get; set; } = false;

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public LibroDto LibroDto { get; set; }

        public string Status { get; set; }
    }
}
