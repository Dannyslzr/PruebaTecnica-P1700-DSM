using Microsoft.EntityFrameworkCore;
using Models.Dtos.Empleados;
using Models.Entities;
using Services.Interfaces;
using Services.UnitOfWork;
using System.Data;
using System.Text;

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

        public async Task<IEnumerable<EmpleadosDto>> ObtieneListaEmpleados(string idTienda)
        {
            try
            {

                var predicate = PredicateBuilder.True<Empleados>();

                if (!string.IsNullOrEmpty(idTienda))
                    predicate = predicate.And(x => x.IdTienda.Equals(idTienda));

                predicate = predicate.And(x => x.IndEliminado == false);


                var lst = await _unitOfWork.GetRepository<Empleados>()
                                           .All
                                           .Where(predicate)
                                           .ToListAsync();

                var result = new List<EmpleadosDto>();
                foreach (var ln in lst)
                {
                    result.Add(new EmpleadosDto
                    {
                        IdEmpleado = ln.IdEmpleado,
                        IdTienda = ln.IdTienda,
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

        public async Task<EmpleadosDto> ObtieneEmpleadoXId(string idEmpleado)
        {
            try
            {
                var empleado = await _unitOfWork.GetRepository<Empleados>()
                                           .All
                                           .Where(x => x.IdEmpleado.Equals(idEmpleado))
                                           .FirstOrDefaultAsync();

                var result = new EmpleadosDto
                {
                    IdEmpleado = empleado.IdEmpleado,
                    IdTienda = empleado.IdTienda,
                    Identificacion = empleado.Identificacion,
                    Nombre = empleado.Nombre,
                    Apellido1 = empleado.Apellido1,
                    Apellido2 = empleado.Apellido2,
                    Telefono = empleado.Telefono,
                    TipoEmpleado = empleado.TipoEmpleado,
                    IdSupervisor = empleado.IdSupervisor,
                    Salario = empleado.Salario,
                    UCreador = empleado.UCreador,
                    FechaCreacion = empleado.FechaCreacion,
                    UActualiza = empleado.UActualiza,
                    FechaActualiza = empleado.FechaActualiza
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("No es posible obtener de empleado por id");
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
                        Nombre = ln.Nombre + " " + ln.Apellido1 + " " + ln.Apellido2
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
                    IdTienda = dto.IdTienda,
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

        public async Task<bool> ActualizarEmpleadoAsync(EmpleadosDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var empleado = await _unitOfWork.GetRepository<Empleados>()
                    .All
                    .Where(x => x.IdEmpleado.Equals(dto.IdEmpleado))
                    .FirstAsync();

                empleado.IdTienda = dto.IdTienda;
                empleado.Identificacion = dto.Identificacion;
                empleado.Nombre = dto.Nombre;
                empleado.Apellido1 = dto.Apellido1;
                empleado.Apellido2 = dto.Apellido2;
                empleado.Telefono = dto.Telefono;
                empleado.TipoEmpleado = dto.TipoEmpleado;
                empleado.IdSupervisor = dto.IdSupervisor;
                empleado.Salario = dto.Salario;
                empleado.UActualiza = dto.UActualiza;
                empleado.FechaActualiza = await _utilidades.ObtenerFecha();

                var repository = _unitOfWork.GetRepository<Empleados>();
                repository.Update(empleado);
                await _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return false;
            }
        }

        public async Task<bool> EliminarEmpleadoAsync(EmpleadosDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var empleado = await _unitOfWork.GetRepository<Empleados>()
                    .All
                    .Where(x => x.IdEmpleado.Equals(dto.IdEmpleado))
                    .FirstAsync();

                empleado.IndEliminado = true;
                empleado.FechaActualiza = await _utilidades.ObtenerFecha();

                var repository = _unitOfWork.GetRepository<Empleados>();
                repository.Update(empleado);
                await _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return false;
            }
        }

        public async Task<List<ConsultaEmpleadosModel>> ConsultaEmpleadosSp(string idEmpleado, string conn)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("EXEC ObtenerEmpleados ");
                sql.AppendLine($"@IdEmpleado = '{idEmpleado}'");

                var dt = await _unitOfWork.ExecuteQuery(conn, sql.ToString());
                var result = new List<ConsultaEmpleadosModel>();

                foreach (DataRow dr in dt.Rows)
                {
                    result.Add(new ConsultaEmpleadosModel()
                    {
                        NombreEmpleado = dr["Nombre del Empleado"].ToString(),
                        NombreSupervisor = dr["Nombre del Supervidor"].ToString(),
                        FechaCreacion = DateTime.Parse(dr["Fecha Creación"].ToString()),
                        Salario = decimal.Parse(dr["Salario"].ToString()),
                        Telefono = dr["Teléfono"].ToString(),
                        TipoEmpleado = dr["Tipo de empleado"].ToString()
                    });
                }

                return result;
            }
            catch (Exception EX)
            {
                throw new Exception("Error al realizar consulta de SP ObtenerEmpleados");
            }
        }
    }
}
