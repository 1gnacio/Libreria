using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
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
        public virtual ICollection<LibroImagenDto> LibroImagen { get; set; }
        public List<string> ImagenesUrl { get; set; }

    }
}
