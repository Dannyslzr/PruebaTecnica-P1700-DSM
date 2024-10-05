using GST.Web.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dtos.Empleados;
using Models.Dtos.Results;
using System.Security.Policy;
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
                    LstEmpleadosSelect = new SelectList(result.ValueElement.ToList(), "IdEmpleado", "Nombre")
                };

                return PartialView(emp);
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
                //if (!ModelState.IsValid) return PartialView(model);
                model.UCreador = "9471C2CF-0B71-4BD4-89BE-33D649D59DAF";
                model.FechaCreacion = new DateTime();
                model.FechaActualiza = new DateTime();
                model.IdEmpleado = "";
                model.UActualiza = "";

                var url = ApiData.URL + $"Empleados/GuardaNuevoEmpleado/";
                var result = await _utils.PostItemGetItem<EmpleadosDto, bool>(url, model, "");

                

                if (result.IsSuccess)
                {
                    //_notify.Success("Empresa registrada correctamente", 5);
                    return RedirectToAction("Index");
                }
                else
                {
                    //_notify.Warning("Por favor intente nuevamente", 5);
                    return PartialView("CrearPartial",model);
                }
            }
            catch (Exception ex)
            {
                //_notify.Error("Ha sucedido un error: " + ex.Message, 5);
                return PartialView(model);
            }
        }

    }
}
