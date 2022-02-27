using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProductoCliente.Service.IService
{
    public interface IAuthenticationService
    {
        Task<RegistroResponseDto> RegistroUsuario(UsuarioRequestDto usuarioRegistro);
        Task<AutenticacionResponseDto> Login(AutenticacionDto usuarioAutenticacion);
        Task Logout();
    }
}
