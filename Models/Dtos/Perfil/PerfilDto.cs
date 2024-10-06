using Models.Dtos.Permisos;

namespace Models.Dtos.Perfil
{
    public class PerfilDto
    {
        public string IdPerfil { get; set; }
        public string Descripcion { get; set; }

        public List<PermisosDto> Permisos { get; set; }
    }
}
