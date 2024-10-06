using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Empleados
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string IdEmpleado { get; set; }
        [Required]
        [StringLength(50)]
        public string IdTienda { get; set; }
        [Required]
        [StringLength(15)]
        public string Identificacion { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(30)]
        public string Apellido1 { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido2 { get; set; }
        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public string TipoEmpleado { get; set; }
        [Required]
        [StringLength(50)]
        public string IdSupervisor { get; set; }
        [Required]
        public decimal Salario { get; set; }
        [Required]
        [StringLength(50)]
        public string UCreador { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public string? UActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }
        public bool IndEliminado { get; set; }
    }
}
