using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Models.Dtos.Usuario
{
    public class UsuarioDto
    {
        public string IdUsuario { get; set; }
        [DisplayName("Tienda")]
        public string IdTienda { get; set; }
        [DisplayName("Perfil")]
        public string IdPerfil { get; set; }
        [DisplayName("Identitificación")]
        public string Identificacion { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        [DisplayName("Primer apellido")]
        public string Apellido1 { get; set; }
        [DisplayName("Segundo apellido")]
        public string Apellido2 { get; set; }
        [DisplayName("Correo")]
        public string Correo { get; set; }
        [DisplayName("Contraseña")]
        public string Contrasenna { get; set; }
        [DisplayName("Repetir contraseña")]
        public string RepetirContrasenna { get; set; }
        public SelectList? LstTiendasSelect { get; set; }
        public SelectList? LstPerfilesSelect { get; set; }
    }
}
