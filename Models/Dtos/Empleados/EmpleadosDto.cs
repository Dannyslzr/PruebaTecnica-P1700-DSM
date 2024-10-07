using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Models.Dtos.Empleados
{
    public class EmpleadosDto
    {
        public string IdEmpleado { get; set; }

        [DisplayName("Tienda")]
        public string IdTienda { get; set; }

        [DisplayName("Identificación")]
        public string Identificacion { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Primer apellido")]
        public string Apellido1 { get; set; }

        [DisplayName("Segundo apellido")]
        public string Apellido2 { get; set; }

        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [DisplayName("Tipo de empleado")]
        public string TipoEmpleado { get; set; }

        [DisplayName("Supervisor")]
        public string IdSupervisor { get; set; }

        [DisplayName("Salario")]
        public decimal Salario { get; set; }
        public string UCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        public SelectList? LstEmpleadosSelect { get; set; }
        public SelectList? LstTiendasSelect { get; set; }
        public string ModoEdicion { get; set; }
    }
}
