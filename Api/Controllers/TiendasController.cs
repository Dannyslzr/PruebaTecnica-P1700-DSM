using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Results;
using Models.Dtos.Tiendas;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TiendasController : Controller
    {
        private readonly ITiendas _tiendas;
        public TiendasController(ITiendas tiendas)
        {
            _tiendas = tiendas;
        }

        [HttpGet]
        [Route("ObtenerListaTiendas")]
        public async Task<IActionResult> ObtenerListaEmpleadosAsync()
        {
            try
            {
                var lst = await _tiendas.ObtieneListaTiendas();
                return Ok(Result<IEnumerable<TiendasDto>>.Success(lst, "Tiendas consultados correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible consultar tiendas en este momento"));
            }
        }
    }
}
