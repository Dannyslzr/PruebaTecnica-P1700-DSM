using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Empleados;
using Models.Dtos.Results;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadosController : Controller
    {
        private readonly IEmpleados _empleados;
        public EmpleadosController(IEmpleados empleados)
        {
            _empleados = empleados;
        }

        [HttpGet]
        [Route("ObtenerListaEmpleados/{idTienda}")]
        public async Task<IActionResult> ObtenerListaEmpleadosAsync(string idTienda)
        {
            try
            {
                var lst = await _empleados.ObtieneListaEmpleados();
                return Ok(Result<IEnumerable<EmpleadosDto>>.Success(lst,"Empleados consultados correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible consultar empleados en este momento"));
            }
        }

        [HttpGet]
        [Route("ObtenerListaEmpleadosDll/{idTienda}")]
        public async Task<IActionResult> ObtenerListaEmpleadosDllAsync(string idTienda)
        {
            try
            {
                var lst = await _empleados.ObtieneListaEmpleadosDll();
                return Ok(Result<IEnumerable<EmpleadosDllDto>>.Success(lst, "Empleados consultados correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible consultar empleados en este momento"));
            }
        }

        [HttpPost]
        [Route("GuardaNuevoEmpleado/")]
        public async Task<IActionResult> GuardaNuevoEmpleadoAsync(EmpleadosDto dto)
        {
            try
            {
                var result = await _empleados.GuardaNuevoEmpleadoAsync(dto);
                return Ok(Result<bool>.Success(result, "Empleado registrado correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible registrar empleados en este momento"));
            }
        }
    }
}
