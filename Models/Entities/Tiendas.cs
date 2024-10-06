using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Tiendas
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string IdTienda { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
    }
}
