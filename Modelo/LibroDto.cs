using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class LibroDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingresa nombre de libro")]
        public string Nombre { get; set; }

        [Range(10, 100, ErrorMessage = "El precio regular debe estar entre 10 y 100 dolares")]
        public double PrecioRegular { get; set; }

        [Required(ErrorMessage = "Por favor ingresa nombre de autor")]
        public string Autor { get; set; }
        public string Detalles { get; set; }
    }
}
