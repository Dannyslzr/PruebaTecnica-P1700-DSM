using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.Jwt;
using Models.Dtos.Results;
using Models.Dtos.Usuario;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("User/")]
    public class UsuariosController : Controller
    {
        public IConfiguration _config;
        private readonly IUsuarios _usuarios;
        public UsuariosController(IConfiguration configuration, IUsuarios usuarios)
        {
            _config = configuration;
            _usuarios = usuarios;
        }

        
        [HttpPost]
        public async Task<IActionResult> CrearUsuarioAsync(UsuarioDto dto)
        {
            try
            {

                throw new Exception("praa");
                //var result = _usuarios.CrearUsuarioAsync(dto);

                //if (!result)
                //{

                //}

                //return Request.CreateResponse(HttpStatusCode.BadRequest,
                //                                  Result<boo>.Success(result, "Asociados"));
            }
            catch (Exception ex)
            {
                return BadRequest(Result<bool>.Failure("No es posible guardar miembro en este momento"));
            }
           
        }

        [HttpPost("IniciarSesion")]
        public async Task<Result<UsuarioInicioSesionDto>> IniciarSesion(InicioSesionDto inicioSesion)
        {
            try
            {
                //Se reliza consulta al api por usuario y contrasenha
                var usuario = await _usuarios.ValidaUsuarioSesionAsync(inicioSesion.Correo, inicioSesion.Contrasena);

                if (usuario == null)
                    return Result<UsuarioInicioSesionDto>.Failure("Usuario o contraseña incorrecta");

                var jwt = _config.GetSection("Jwt").Get<JwtDto>();

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
                claims.AddClaim(new Claim("UsuarioId", usuario.Id.ToString()));
                claims.AddClaim(new Claim("UsuarioNombre", usuario.Nombre.ToString()));

                foreach (var role in usuario.Perfil.Permisos)
                    claims.AddClaim(new Claim(ClaimTypes.Role, role.Clave));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddSeconds(jwt.ExpirationSeconds),
                    SigningCredentials = signIn
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                var resultToken = tokenHandler.WriteToken(tokenConfig);

                var model = new UsuarioInicioSesionDto()
                {
                    Id = usuario.Id,
                    IdTienda = usuario.IdTienda,
                    Nombre = usuario.Nombre,
                    jwtToken = resultToken,
                    Perfil = usuario.Perfil,
                };

                return Result<UsuarioInicioSesionDto>.Success(model, "Sesión iniciada correctamente");
            }
            catch (Exception)
            {
                return Result<UsuarioInicioSesionDto>.Failure("No fue posible iniciar sesión");
            }
        }
    }
}
