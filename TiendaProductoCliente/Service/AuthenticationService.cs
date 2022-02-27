using Blazored.LocalStorage;
using Comun;
using Microsoft.AspNetCore.Components.Authorization;
using Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TiendaProductoCliente.Service.IService;

namespace TiendaProductoCliente.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AutenticacionResponseDto> Login(AutenticacionDto usuarioAutenticacion)
        {
            var contenido = JsonConvert.SerializeObject(usuarioAutenticacion);

            var bodyContenido = new StringContent(contenido, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/account/login", bodyContenido);
            var contenidoTemp = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<AutenticacionResponseDto>(contenidoTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, resultado.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, resultado.usuarioDto);
                ((AuthStateProvider)_authStateProvider).NotificarUsuarioLogueado(resultado.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", resultado.Token);
                return new AutenticacionResponseDto
                {
                    AutenticacionExitoso = true
                };
            }
            else
            {
                return resultado;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
            ((AuthStateProvider)_authStateProvider).NotificarUsuarioLogout();
            _client.DefaultRequestHeaders.Authorization = null;

        }

        public async Task<RegistroResponseDto> RegistroUsuario(UsuarioRequestDto usuarioRegistro)
        {
            var contenido = JsonConvert.SerializeObject(usuarioRegistro);

            var bodyContenido = new StringContent(contenido, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/account/registro", bodyContenido);
            var contenidoTemp = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<RegistroResponseDto>(contenidoTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegistroResponseDto
                {
                    RegistroExitoso = true
                };
            }
            else
            {
                return resultado;
            }
        }
    }
}
