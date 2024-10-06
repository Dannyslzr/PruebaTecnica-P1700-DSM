using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PerfilPermisos
    {
        public string Id { get; set; }
        public string IdPerfil { get; set; }
        public string IdPermiso { get; set; }

        [ForeignKey("IdPermiso")]
        public Permisos Permiso { get; set; }
    }
}
