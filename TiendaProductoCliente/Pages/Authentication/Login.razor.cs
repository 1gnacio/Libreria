using Microsoft.AspNetCore.Components;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TiendaProductoCliente.Service.IService;

namespace TiendaProductoCliente.Pages.Authentication
{
    public partial class Login
    {
        private AutenticacionDto UsuarioFormAutenticacion = new AutenticacionDto();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarAutenticacionErrores { get; set; }
        public string Errores { get; set; }
        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task LoginUsuario()
        {
            MostrarAutenticacionErrores = false;
            EstaProcesando = true;

            var resultado = await authenticationService.Login(UsuarioFormAutenticacion);

            if (resultado.AutenticacionExitoso)
            {
                EstaProcesando = false;
                var absolutUri = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absolutUri.Query);
                ReturnUrl = queryParam["returnUrl"];

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }

            }
            else
            {
                EstaProcesando = false;
                Errores = resultado.ErrorMensaje;
                MostrarAutenticacionErrores = true;
            }
        }
    }
}
