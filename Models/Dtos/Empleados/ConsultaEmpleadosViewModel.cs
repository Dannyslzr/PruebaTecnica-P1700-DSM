using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Dtos.Empleados
{
    public class ConsultaEmpleadosViewModel
    {
        public string IdEmpleado { get; set; }

        public SelectList? LstEmpleadosSelect { get; set; }

        public List<ConsultaEmpleadosModel> Detalle { get; set; }
    }
}
