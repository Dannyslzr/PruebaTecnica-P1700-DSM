using Models.Dtos.Perfil;

namespace Models.Dtos.Usuario
{
    public class UsuarioInicioSesionDto
    {
        public string Id { get; set; }
        public string IdTienda { get; set; }
        public string TiendaNombre { get; set; }
        public string Nombre { get; set; }
        public PerfilDto Perfil { get; set; }
        public string jwtToken { get; set; }

    }
}
