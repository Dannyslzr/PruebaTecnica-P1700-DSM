using Microsoft.EntityFrameworkCore;
using Models.Dtos.Empleados;
using Models.Dtos.Perfil;
using Models.Entities;
using Services.Interfaces;
using Services.UnitOfWork;

namespace Services.Servicios
{
    public class PerfilService : IPerfil
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilidades _utilidades;

        public PerfilService(IUnitOfWork unitOfWork, IUtilidades utilidades)
        {
            _unitOfWork = unitOfWork;
            _utilidades = utilidades;
        }

        public async Task<List<PerfilDllDto>> ObtieneListaPerfilesDll()
        {

            try
            {
                var lst = await _unitOfWork.GetRepository<Perfil>().All.Select(a => new
                {
                    a.IdPerfil,
                    a.Descripcion,
                }).ToListAsync();

                var result = new List<PerfilDllDto>();
                foreach (var ln in lst)
                {
                    result.Add(new PerfilDllDto
                    {
                        IdPerfil = ln.IdPerfil,
                        Descripcion = ln.Descripcion
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar lista de perfiles para dll");
            }
        }
    }
}
