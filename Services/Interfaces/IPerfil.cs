using Models.Dtos.Perfil;

namespace Services.Interfaces
{
    public interface IPerfil
    {
        Task<List<PerfilDllDto>> ObtieneListaPerfilesDll();
    }
}
