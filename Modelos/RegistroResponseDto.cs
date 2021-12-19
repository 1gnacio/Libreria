using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class RegistroResponseDto
    {
        public bool RegistroExitoso { get; set; }

        public IEnumerable<string> Errores { get; set; }

    }
}
