using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class LibroImagen
    {
        public int Id { get; set; }

        public int LibroId { get; set; }

        public string LibroImagenUrl { get; set; }

        [ForeignKey("LibroId")]
        public virtual Libro Libro { get; set; }
    }
}
