using Models.Dtos.Empleados;

namespace Services.Interfaces
{
    public interface IEmpleados
    {
        Task<IEnumerable<EmpleadosDto>> ObtieneListaEmpleados(string idTienda);
        Task<EmpleadosDto> ObtieneEmpleadoXId(string idEmpleado);
        Task<IEnumerable<EmpleadosDllDto>> ObtieneListaEmpleadosDll();
        Task<bool> GuardaNuevoEmpleadoAsync(EmpleadosDto dto);
        Task<bool> ActualizarEmpleadoAsync(EmpleadosDto dto);
        Task<bool> EliminarEmpleadoAsync(EmpleadosDto dto);
        Task<List<ConsultaEmpleadosModel>> ConsultaEmpleadosSp(string idEmpleado, string conn);
    }
}
