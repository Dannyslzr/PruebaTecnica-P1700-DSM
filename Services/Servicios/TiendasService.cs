using Microsoft.EntityFrameworkCore;
using Models.Dtos.Empleados;
using Models.Dtos.Tiendas;
using Models.Entities;
using Services.Interfaces;
using Services.UnitOfWork;

namespace Services.Servicios
{
    public class TiendasService : ITiendas
    {
        private readonly IUnitOfWork _unitOfWork;

        public TiendasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TiendasDto>> ObtieneListaTiendas()
        {
            try
            {
                var lst = await _unitOfWork.GetRepository<Tiendas>()
                                           .All
                                           .ToListAsync();

                var result = new List<TiendasDto>();
                foreach (var ln in lst)
                {
                    result.Add(new TiendasDto
                    {
                        IdTienda = ln.IdTienda,
                        Nombre = ln.Nombre,
                       
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("No es posible obtener listado de tiendas");
            }
        }
    }
}
