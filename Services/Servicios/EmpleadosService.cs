using Microsoft.EntityFrameworkCore;
using Models.Dtos.Empleados;
using Models.Dtos.Results;
using Models.Entities;
using Services.Interfaces;
using Services.UnitOfWork;

namespace Services.Servicios
{
    public class EmpleadosService : IEmpleados
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilidades _utilidades;

        public EmpleadosService(IUnitOfWork unitOfWork, IUtilidades utilidades)
        {
            _unitOfWork = unitOfWork;
            _utilidades = utilidades;
        }

        public async Task<IEnumerable<EmpleadosDto>> ObtieneListaEmpleados()
        {
            try
            {
                var lst = await _unitOfWork.GetRepository<Empleados>().All.ToListAsync();
                var result = new List<EmpleadosDto>();
                foreach (var ln in lst)
                {
                    result.Add(new EmpleadosDto
                    {
                        IdEmpleado = ln.IdEmpleado,
                        Identificacion = ln.Identificacion,
                        Nombre = ln.Nombre,
                        Apellido1 = ln.Apellido1,
                        Apellido2 = ln.Apellido2,
                        Telefono = ln.Telefono,
                        TipoEmpleado = ln.TipoEmpleado,
                        IdSupervisor = ln.IdSupervisor,
                        Salario = ln.Salario,
                        UCreador = ln.UCreador,
                        FechaCreacion = ln.FechaCreacion,
                        UActualiza = ln.UActualiza,
                        FechaActualiza = ln.FechaActualiza
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("No es posible obtener listado de empleados");
            }
        }

        public async Task<IEnumerable<EmpleadosDllDto>> ObtieneListaEmpleadosDll()
        {
            try
            {
                var lst = await _unitOfWork.GetRepository<Empleados>().All.Select(a => new
                {
                    a.IdEmpleado,
                    a.Nombre,
                    a.Apellido1,
                    a.Apellido2,
                }).ToListAsync();

                var result = new List<EmpleadosDllDto>();
                foreach (var ln in lst)
                {
                    result.Add(new EmpleadosDllDto
                    {
                        IdEmpleado = ln.IdEmpleado,
                        Nombre = ln.Nombre + " " +ln.Apellido1 + " " + ln.Apellido2
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("No es posible obtener listado de empleados para dll");
            }
        }

        public async Task<bool> GuardaNuevoEmpleadoAsync(EmpleadosDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var entity = new Empleados()
                {
                    IdEmpleado = Guid.NewGuid().ToString(),
                    Identificacion = dto.Identificacion,
                    Nombre = dto.Nombre,
                    Apellido1 = dto.Apellido1,
                    Apellido2 = dto.Apellido2,
                    Telefono = dto.Telefono,        
                    Salario = dto.Salario,
                    TipoEmpleado = dto.TipoEmpleado,
                    IdSupervisor = dto.IdSupervisor,
                    UCreador = dto.UCreador,
                    FechaCreacion = await _utilidades.ObtenerFecha(),
                    FechaActualiza = null,
                    UActualiza = null
                };

                var repository = _unitOfWork.GetRepository<Empleados>();
                repository.Add(entity);
                await _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return false;
            }
        }
    }
}
