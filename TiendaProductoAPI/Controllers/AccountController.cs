using AccesoDatos.Data;
using Comun;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modelos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TiendaProductoAPI.Helper;

namespace TiendaProductoAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AplicacionUser> _signInManager;
        private readonly UserManager<AplicacionUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApiSettings _apiSettings;

        public AccountController(SignInManager<AplicacionUser> signInManager, 
            UserManager<AplicacionUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<ApiSettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _apiSettings = options.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro([FromBody] UsuarioRequestDto usuarioRequestDto)
        {
            if(usuarioRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var usuario = new AplicacionUser()
            {
                UserName = usuarioRequestDto.Email,
                Name = usuarioRequestDto.Name,
                Email = usuarioRequestDto.Email,
                PhoneNumber = usuarioRequestDto.PhoneNo,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(usuario, usuarioRequestDto.Password);

            if (!resultado.Succeeded)
            {
                var errores = resultado.Errors.Select(x => x.Description);
                return BadRequest(new RegistroResponseDto()
                {
                    Errores = errores,
                    RegistroExitoso = false
                });
            }

            var roleResultado = await _userManager.AddToRoleAsync(usuario, SD.RoleCliente);

            if (!roleResultado.Succeeded)
            {
                var errores = roleResultado.Errors.Select(x => x.Description);
                return BadRequest(new RegistroResponseDto()
                {
                    Errores = errores,
                    RegistroExitoso = false
                });
            }

            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AutenticacionDto autenticacionDto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(autenticacionDto.UserName, autenticacionDto.Password, false, false);

            if (resultado.Succeeded)
            {
                var usuario = await _userManager.FindByNameAsync(autenticacionDto.UserName);

                if(usuario == null)
                {
                    return Unauthorized(new AutenticacionResponseDto()
                    {
                        AutenticacionExitoso = false,
                        ErrorMensaje = "Autenticacion invalida"
                    });
                }

                var signInCredentials = GetSigningCredentials();

                var claims = await GetClaims(usuario);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidIssuer,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signInCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AutenticacionResponseDto()
                {
                    AutenticacionExitoso = true,
                    Token = token,
                    usuarioDto = new UsuarioDto()
                    {
                        Name = usuario.Name,
                        Id = usuario.Id,
                        Email = usuario.Email,
                        PhoneNo = usuario.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new AutenticacionResponseDto()
                {
                    AutenticacionExitoso = false,
                    ErrorMensaje = "Autenticacion invalida"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(AplicacionUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)

            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
        
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }   
}
