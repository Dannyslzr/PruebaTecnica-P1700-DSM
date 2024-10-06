using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dtos.Empleados;
using Web.Utilidades;

namespace P1700_DSM.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IUtilidades _utils;

        public EmpleadosController(IUtilidades utils)
        {
            _utils = utils;
        }

        public async Task<IActionResult> Index()
        {
            var url = ApiData.URL + $"Empleados/ObtenerListaEmpleados/{"111"}";
            var result = await _utils.GetAsync<IEnumerable<EmpleadosDto>>(url, "");

            if (!result.IsSuccess)
            {
               //tirar error
            }

            var model = new EmpleadosViewModel()
            {
                LstEmpleados = result.ValueElement.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> CrearPartial()
        {
            try
            {
                var url = ApiData.URL + $"Empleados/ObtenerListaEmpleadosDll/{"111"}";
                var result = await _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(url, "");
                var emp = new EmpleadosDto()
                {
                    LstEmpleadosSelect = new SelectList(result.ValueElement.ToList(), "IdEmpleado", "Nombre"),
                    ModoEdicion = "Crear"
                };

                return PartialView("EmpleadoModalPartial", emp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EmpleadosDto model)
        {
            try
            {
                model.UCreador = "9471C2CF-0B71-4BD4-89BE-33D649D59DAF";
                model.FechaCreacion = new DateTime();
                model.FechaActualiza = new DateTime();
                model.IdEmpleado = "";
                model.UActualiza = "";

                var url = ApiData.URL + $"Empleados/GuardaNuevoEmpleado/";
                var result = await _utils.PostItemGetItem<EmpleadosDto, bool>(url, model, "");

                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return PartialView("EmpleadoModalPartial", model);
                }
            }
            catch (Exception ex)
            {
                return PartialView(model);
            }
        }

        public async Task<IActionResult> ActualizarPartial(string idEmpleado)
        {
            try
            {
                var urlEmpleado = ApiData.URL + $"Empleados/ObtenerEmpleadosXId/{idEmpleado}";
                //var resultEmpleado = await _utils.GetAsync<EmpleadosDto>(urlEmpleado, "");
                var tskResultEmpleado = _utils.GetAsync<EmpleadosDto>(urlEmpleado, "");

                var urlDll = ApiData.URL + $"Empleados/ObtenerListaEmpleadosDll/{"111"}";
                //var resultDll = await _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(urlDll, "");
                var tskResultDll = _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(urlDll, "");

                await Task.WhenAll(tskResultEmpleado, tskResultDll);
                var emp = new EmpleadosDto();
                emp = tskResultEmpleado.Result.ValueElement;
                emp.LstEmpleadosSelect = new SelectList(tskResultDll.Result.ValueElement.ToList(), "IdEmpleado", "Nombre");
                emp.ModoEdicion = "Editar";

                return PartialView("EmpleadoModalPartial", emp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Actualizar(EmpleadosDto model)
        {
            try
            {
                model.UCreador = "9471C2CF-0B71-4BD4-89BE-33D649D59DAF";
                model.FechaCreacion = new DateTime();
                model.FechaActualiza = new DateTime();
                model.UActualiza = model.UCreador;

                var url = ApiData.URL + $"Empleados/ActualizarEmpleado/";
                var result = await _utils.PostItemGetItem<EmpleadosDto, bool>(url, model, "");

                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return PartialView("EmpleadoModalPartial", model);
                }
            }
            catch (Exception ex)
            {
                return PartialView(model);
            }
        }
    }
}
