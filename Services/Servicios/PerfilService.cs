using Microsoft.EntityFrameworkCore;
using Models.Dtos.Perfil;
using Models.Dtos.Permisos;
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
        public async Task<List<PerfilDto>> ObtieneListaPerfilUsuario()
        {
            try
            {
                var lstResult = new List<PerfilDto>();

                var perfilResult = await _unitOfWork.GetRepository<Perfil>().All.ToListAsync();

                foreach (var ln in perfilResult)
                {
                    var perfil = new PerfilDto()
                    {
                        IdPerfil = ln.IdPerfil,
                        Descripcion = ln.Descripcion,
                    };

                    perfil.Permisos = new List<PermisosDto>();

                    var perfilPermisosResult = await _unitOfWork.GetRepository<PerfilPermisos>().AllIncluding(per => per.Permiso)
                                                                            .Where(x => x.IdPerfil == ln.IdPerfil)
                                                                            .ToListAsync();
                    foreach (var permiso in perfilPermisosResult)
                    {
                        perfil.Permisos.Add(new PermisosDto
                        {
                            IdPermiso = permiso.IdPermiso,
                            Clave = permiso.Permiso.Clave,
                            Descripcion = permiso.Permiso.Descripcion
                        });
                    }

                    lstResult.Add(perfil);
                }

                return lstResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
