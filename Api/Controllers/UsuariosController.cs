using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Results;
using Models.Dtos.Usuario;
using Services.Interfaces;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("User/")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarios _usuarios;
        public UsuariosController(IUsuarios usuarios)
        {
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
    }
}
