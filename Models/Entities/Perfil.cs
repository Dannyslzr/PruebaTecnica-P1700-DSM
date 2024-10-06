using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Perfil
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string IdPerfil { get; set; }

        [StringLength(50)]
        [Required]
        public string Descripcion { get; set; }


        [ForeignKey("IdPerfil")]
        public PerfilPermisos PerfilPermisos { get; set; }
    }
}
