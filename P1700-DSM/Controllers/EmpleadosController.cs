﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dtos.Empleados;
using Models.Dtos.Perfil;
using Models.Dtos.Tiendas;
using Web.Utilidades;

namespace P1700_DSM.Controllers
{
    [Authorize]
    public class EmpleadosController : Controller
    {
        private readonly IUtilidades _utils;

        public EmpleadosController(IUtilidades utils)
        {
            _utils = utils;
        }

        [Authorize(Roles = "RegEmp")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var idTienda = User.FindFirst("IdTienda")?.Value;
            var url = ApiData.URL + $"Empleados/ObtenerListaEmpleados/{idTienda}";
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

        [Authorize(Roles = "RegEmp")]
        public async Task<IActionResult> CrearPartial()
        {
            try
            {
                var idTienda = User.FindFirst("IdTienda")?.Value;
                var url = ApiData.URL + $"Empleados/ObtenerListaEmpleadosDll/{idTienda}";
                var result = await _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(url, "");

                var urlTiendas = ApiData.URL + $"Tiendas/ObtenerListaTiendas";
                var resultTiendas = await _utils.GetAsync<IEnumerable<TiendasDto>>(urlTiendas, "");


                var emp = new EmpleadosDto()
                {
                    LstEmpleadosSelect = new SelectList(result.ValueElement.ToList(), "IdEmpleado", "Nombre"),
                    LstTiendasSelect = new SelectList(resultTiendas.ValueElement.ToList(), "IdTienda", "Nombre"),
                    ModoEdicion = "Crear"
                };

                return PartialView("EmpleadoModalPartial", emp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "RegEmp")]
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

        [Authorize(Roles = "RegEmp")]
        public async Task<IActionResult> ActualizarPartial(string idEmpleado)
        {
            try
            {
                var idTienda = User.FindFirst("IdTienda")?.Value;
                var urlEmpleado = ApiData.URL + $"Empleados/ObtenerEmpleadosXId/{idEmpleado}";
                var tskResultEmpleado = _utils.GetAsync<EmpleadosDto>(urlEmpleado, "");
                var urlDll = ApiData.URL + $"Empleados/ObtenerListaEmpleadosDll/{idTienda}";
                var tskResultDll = _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(urlDll, "");
                var urlDllTiendas = ApiData.URL + $"Tiendas/ObtenerListaTiendas/";
                var tskResultDllTiendas = _utils.GetAsync<IEnumerable<TiendasDto>>(urlDllTiendas, "");

                await Task.WhenAll(tskResultEmpleado, tskResultDll, tskResultDllTiendas);
                var emp = new EmpleadosDto();
                emp = tskResultEmpleado.Result.ValueElement;
                emp.LstEmpleadosSelect = new SelectList(tskResultDll.Result.ValueElement.ToList(), "IdEmpleado", "Nombre");
                emp.LstTiendasSelect = new SelectList(tskResultDllTiendas.Result.ValueElement.ToList(), "IdTienda", "Nombre");
                emp.ModoEdicion = "Editar";
                return PartialView("EmpleadoModalPartial", emp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "RegEmp")]
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

        [Authorize(Roles = "RegEmp")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(string idEmpleado)
        {
            try
            {
                var urlEmpleado = ApiData.URL + $"Empleados/ObtenerEmpleadosXId/{idEmpleado}";
                var resultEmpleado = await _utils.GetAsync<EmpleadosDto>(urlEmpleado, "");
                var empleado = resultEmpleado.ValueElement;

                empleado.UActualiza = "9471C2CF-0B71-4BD4-89BE-33D649D59DAF";
                empleado.ModoEdicion = "Eliminar";

                var url = ApiData.URL + $"Empleados/EliminarEmpleado/";
                var result = await _utils.PostItemGetItem<EmpleadosDto, bool>(url, empleado, "");

                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }

                return PartialView("EmpleadoModalPartial");
            }
            catch (Exception)
            {
                return PartialView();
            }
        }

        [Authorize(Roles = "ConEmp")]
        [HttpGet]
        public async Task<IActionResult> ConsultaEmpleados()
        {
            var urlConsulta = ApiData.URL + $"Empleados/ObtieneConsultaEmpleados/todos";
            var tskResultConsulta = _utils.GetAsync<IEnumerable<ConsultaEmpleadosModel>>(urlConsulta, "");
            var idTienda = User.FindFirst("IdTienda")?.Value;
            var urlDll = ApiData.URL + $"Empleados/ObtenerListaEmpleadosDll/{idTienda}";
            var tskResultEmpleadosDll = _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(urlDll, "");

            await Task.WhenAll(tskResultEmpleadosDll, tskResultConsulta);

            if (!tskResultConsulta.Result.IsSuccess)
            {
                //tirar error
            }

            var model = new ConsultaEmpleadosViewModel()
            {
                LstEmpleadosSelect = new SelectList(tskResultEmpleadosDll.Result.ValueElement.ToList(), "IdEmpleado", "Nombre"),
                Detalle = tskResultConsulta.Result.ValueElement.ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "ConEmp")]
        [HttpPost]
        public async Task<IActionResult> ConsultaEmpleados(ConsultaEmpleadosViewModel dto)
        {
            var urlConsulta = ApiData.URL + $"Empleados/ObtieneConsultaEmpleados/" + dto.IdEmpleado;
            var tskResultConsulta = _utils.GetAsync<IEnumerable<ConsultaEmpleadosModel>>(urlConsulta, "");
            var idTienda = User.FindFirst("IdTienda")?.Value;
            var urlDll = ApiData.URL + $"Empleados/ObtenerListaEmpleadosDll/{idTienda}";
            var tskResultEmpleadosDll = _utils.GetAsync<IEnumerable<EmpleadosDllDto>>(urlDll, "");

            await Task.WhenAll(tskResultEmpleadosDll, tskResultConsulta);

            if (!tskResultConsulta.Result.IsSuccess)
            {
                //tirar error
            }

            dto.LstEmpleadosSelect = new SelectList(tskResultEmpleadosDll.Result.ValueElement.ToList(), "IdEmpleado", "Nombre");
            dto.Detalle = tskResultConsulta.Result.ValueElement.ToList();

            return View(dto);
        }

        [Authorize(Roles = "ConPer")]
        [HttpGet]
        public async Task<IActionResult> ConsultaPerfiles()
        {
            var url = ApiData.URL + $"Empleados/ObtieneListaPerfiles";
            var result = await _utils.GetAsync<List<PerfilDto>>(url, "");

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            var dto = new ConsultaPerfilesViewModel()
            {
                Perfiles = result.ValueElement.ToList()
            };

            return View(dto);
        }
    }
}
