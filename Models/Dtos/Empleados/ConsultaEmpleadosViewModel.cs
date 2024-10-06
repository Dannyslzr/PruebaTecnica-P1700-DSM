using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Models.Dtos.Empleados
{
    public class ConsultaEmpleadosViewModel
    {
        [DisplayName("Empleado")]
        public string IdEmpleado { get; set; }

        public SelectList? LstEmpleadosSelect { get; set; }

        public List<ConsultaEmpleadosModel> Detalle { get; set; }
    }
}
