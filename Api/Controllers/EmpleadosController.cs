using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Empleados;
using Models.Dtos.Perfil;
using Models.Dtos.Results;
using Services.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadosController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IEmpleados _empleados;
        private readonly IPerfil _perfil;
        public EmpleadosController(IConfiguration config, IEmpleados empleados, IPerfil perfil)
        {
            _config = config;
            _empleados = empleados;
            _perfil = perfil;
        }

        [HttpGet]
        [Route("ObtenerListaEmpleados/{idTienda}")]
        public async Task<IActionResult> ObtenerListaEmpleadosAsync(string idTienda)
        {
            try
            {
                var lst = await _empleados.ObtieneListaEmpleados(idTienda);
                return Ok(Result<IEnumerable<EmpleadosDto>>.Success(lst, "Empleados consultados correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible consultar empleados en este momento"));
            }
        }

        [HttpGet]
        [Route("ObtenerEmpleadosXId/{idEmpleado}")]
        public async Task<IActionResult> ObtenerEmpleadosXIdAsync(string idEmpleado)
        {
            try
            {
                var result = await _empleados.ObtieneEmpleadoXId(idEmpleado);
                return Ok(Result<EmpleadosDto>.Success(result, "Empleado consultado correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible obtener empleado en este momento"));
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

        [HttpPost]
        [Route("ActualizarEmpleado/")]
        public async Task<IActionResult> ActualizarEmpleadoAsync(EmpleadosDto dto)
        {
            try
            {
                var result = await _empleados.ActualizarEmpleadoAsync(dto);
                return Ok(Result<bool>.Success(result, "Empleado actualizado correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible actualizar empleados en este momento"));
            }
        }

        [HttpPost]
        [Route("EliminarEmpleado/")]
        public async Task<IActionResult> EliminarEmpleadoAsync(EmpleadosDto dto)
        {
            try
            {
                var result = await _empleados.EliminarEmpleadoAsync(dto);
                return Ok(Result<bool>.Success(result, "Empleado eliminado correctamente."));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible eliminar empleados en este momento"));
            }
        }

        [HttpGet]
        [Route("ObtieneConsultaEmpleados/{idEmplado}")]
        public async Task<IActionResult> ObtieneConsultaEmpleados(string idEmplado)
        {
            try
            {
                if (idEmplado == "todos")
                {
                    idEmplado = "";
                }

                var strCon = _config.GetConnectionString("DefaultConnection");
                var result = await _empleados.ConsultaEmpleadosSp(idEmplado, strCon);
                return Ok(Result<List<ConsultaEmpleadosModel>>.Success(result, "Consultado correctamente"));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible consultar empleados en este momento"));
            }
        }

        [HttpGet]
        [Route("ObtieneListaPerfiles/")]
        public async Task<IActionResult> ObtieneListaPerfiles()
        {
            try
            {
                var result = await _perfil.ObtieneListaPerfilUsuario();
                return Ok(Result<List<PerfilDto>>.Success(result, "Perfiles consultados correctamente"));
            }
            catch (Exception)
            {
                return BadRequest(Result<bool>.Failure("No es posible consultar perfiles en este momento"));
            }
        }
    }
}
