using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Dtos.Usuario
{
    public class UsuarioDto
    {
        public string IdUsuario { get; set; }
        public string IdTienda { get; set; }
        public string IdPerfil { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string RepetirContrasenna { get; set; }
        public SelectList? LstTiendasSelect { get; set; }
        public SelectList? LstPerfilesSelect { get; set; }
    }
}
