using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Permisos
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string IdPermiso { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }
    }
}
