using Models.Dtos.Empleados;

namespace Services.Interfaces
{
    public interface IEmpleados
    {
        Task<IEnumerable<EmpleadosDto>> ObtieneListaEmpleados();
        Task<IEnumerable<EmpleadosDllDto>> ObtieneListaEmpleadosDll();
        Task<bool> GuardaNuevoEmpleadoAsync(EmpleadosDto dto);
        
    }
}
