using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class LibroPedidosDetalle
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

        [ForeignKey("LibroId")]
        public Libro Libro { get; set; }

        public string Status { get; set; }
    }
}
