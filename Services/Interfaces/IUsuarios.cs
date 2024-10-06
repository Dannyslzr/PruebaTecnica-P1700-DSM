using Models.Dtos.Usuario;

namespace Services.Interfaces
{
    public interface IUsuarios
    {
        Task<bool> CrearUsuarioAsync(UsuarioDto dto);
        Task<UsuarioInicioSesionDto> ValidaUsuarioSesionAsync(string correoStr, string constrasenaStr);
    }
}
