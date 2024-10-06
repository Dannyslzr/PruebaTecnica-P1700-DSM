using Models.Dtos.Tiendas;

namespace Services.Interfaces
{
    public interface ITiendas
    {
        Task<IEnumerable<TiendasDto>> ObtieneListaTiendas();
    }
}
