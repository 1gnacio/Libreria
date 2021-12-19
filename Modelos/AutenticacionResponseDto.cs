using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class AutenticacionResponseDto
    {
        public bool AutenticacionExitoso { get; set; }

        public string ErrorMensaje { get; set; }

        public string Token { get; set; }

        public UsuarioDto usuarioDto { get; set; }
    }
}
