using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Usuarios
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string IdTienda { get; set; }

        [Required]
        [StringLength(50)]
        public string IdPerfil { get; set; }

        [Required]
        [StringLength(20)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido1 { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido2 { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(100)]
        public string Contrasenna { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public string? UActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        [ForeignKey("IdTienda")]
        public Tiendas Tiendas { get; set; }
    }
}
