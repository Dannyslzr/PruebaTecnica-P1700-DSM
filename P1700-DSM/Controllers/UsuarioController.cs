using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dtos.Empleados;
using Models.Dtos.Perfil;
using Models.Dtos.Tiendas;
using Models.Dtos.Usuario;
using System.Security.Claims;
using Web.Utilidades;

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
                var url = ApiData.URL + $"User/Autenticar/";
                var result = await _utils.PostItemGetItem<InicioSesionDto, UsuarioInicioSesionDto>(url, dto, "");

                if (!result.IsSuccess)
                {
                    return BadRequest("Usuario o contraseña incorrectos.");
                }

                var usu = result.ValueElement;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usu.Nombre),
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

        public async Task<IActionResult> RegistroPartial()
        {
            try
            {
                var usuario = new UsuarioDto();

                var urlDllPerfil = ApiData.URL + $"User/ObtenerListaPerfilDll/";
                var tskResultDllPerfil = _utils.GetAsync<IEnumerable<PerfilDllDto>>(urlDllPerfil, "");
                var urlDllTiendas = ApiData.URL + $"Tiendas/ObtenerListaTiendas/";
                var tskResultDllTiendas = _utils.GetAsync<IEnumerable<TiendasDto>>(urlDllTiendas, "");

                await Task.WhenAll(tskResultDllPerfil, tskResultDllTiendas);
                usuario.LstPerfilesSelect = new SelectList(tskResultDllPerfil.Result.ValueElement.ToList(), "IdPerfil", "Descripcion");
                usuario.LstTiendasSelect = new SelectList(tskResultDllTiendas.Result.ValueElement.ToList(), "IdTienda", "Nombre");

                return PartialView("RegistroUsuarioPartial", usuario);
            }
            catch (Exception)
            {
                return BadRequest("No fue posible abrir venta de registro de usuario");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioDto dto)
        {
            try
            {
                if(dto.Contrasenna != dto.RepetirContrasenna)
                {
                    return BadRequest("Contraseñas no concuerdan");
                }

                dto.IdUsuario = "";
                var url = ApiData.URL + $"User/Registrar/";
                var result = await _utils.PostItemGetItem<UsuarioDto, bool>(url, dto, "");

                if (!result.IsSuccess)
                {
                    return BadRequest("No fue posible registrar el usuario en este momento");
                }

                return Ok("Usuario registrado correctamente");
            }
            catch (Exception ex)
            {
                return RedirectToAction("InicioSesion");
            }
        }


        public async Task<IActionResult> Home()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
