using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Empleados;
using Models;
using Models.Dtos.Usuario;
using Web.Utilidades;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace P1700_DSM.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : Controller
    {
        private readonly IUtilidades _utils;

        public UsuarioController(IUtilidades utils)
        {
            _utils = utils;     
        }

        [AllowAnonymous]
        public IActionResult InicioSesion()
		{
			return View(new InicioSesionDto());
		}

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(InicioSesionDto dto)
        {
            try
            {
                dto.RepetirContrasena = "";
                var url = ApiData.URL + $"User/IniciarSesion/";
                var result = await _utils.PostItemGetItem<InicioSesionDto, UsuarioInicioSesionDto>(url, dto, "");

                if (!result.IsSuccess)
                {
                    return RedirectToAction("InicioSesion");
                }

                var usu = result.ValueElement;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, ("").ToUpper()),
                    new Claim(ClaimTypes.Email, ""),
                    new Claim("IdUsuario",usu.Id)
                };

                foreach (var role in usu.Perfil.Permisos)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Clave));
                }

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToAction("Index", "Home");          
            }
            catch (Exception ex)
            {
                return RedirectToAction("InicioSesion");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion()
        {
            try
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToAction("InicioSesion", "Usuario");
            }
            catch (Exception)
            {
                return View(new InicioSesionDto());
            }
        }
    }
}
