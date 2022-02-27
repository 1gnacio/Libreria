using Microsoft.AspNetCore.Components;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaProductoCliente.Service.IService;

namespace TiendaProductoCliente.Pages.Authentication
{
    public partial class Registro
    {
        private UsuarioRequestDto UsuarioFormRegistro = new UsuarioRequestDto();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarRegistroErrores { get; set; }
        public IEnumerable<string> Errores { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task RegistrarUsuario()
        {
            MostrarRegistroErrores = false;
            EstaProcesando = true;

            var resultado = await authenticationService.RegistroUsuario(UsuarioFormRegistro);

            if (resultado.RegistroExitoso)
            {
                EstaProcesando = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                EstaProcesando = false;
                Errores = resultado.Errores;
                MostrarRegistroErrores = true;
            }
        }
    }
}
