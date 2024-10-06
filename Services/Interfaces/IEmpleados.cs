using Models.Dtos.Empleados;

namespace Services.Interfaces
{
    public interface IEmpleados
    {
        Task<IEnumerable<EmpleadosDto>> ObtieneListaEmpleados();
        Task<EmpleadosDto> ObtieneEmpleadoXId(string idEmpleado);
        Task<IEnumerable<EmpleadosDllDto>> ObtieneListaEmpleadosDll();
        Task<bool> GuardaNuevoEmpleadoAsync(EmpleadosDto dto);    
        Task<bool> ActualizarEmpleadoAsync(EmpleadosDto dto);
    }
}
