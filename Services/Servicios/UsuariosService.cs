using Microsoft.EntityFrameworkCore;
using Models.Dtos.Perfil;
using Models.Dtos.Permisos;
using Models.Dtos.Usuario;
using Models.Entities;
using Services.Interfaces;
using Services.UnitOfWork;

namespace Services.Servicios
{
    public class UsuariosService : IUsuarios
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilidades _utilidades;

        public UsuariosService(IUnitOfWork unitOfWork, IUtilidades utilidades)
        {
            _unitOfWork = unitOfWork;
            _utilidades = utilidades;
        }

        public async Task<bool> CrearUsuarioAsync(UsuarioDto dto)
        {
            await _unitOfWork.BeginTransaction();
            try
            {
                var constrasena = await _utilidades.EncriptaString(dto.Contrasenna);
                dto.Contrasenna = constrasena;

                var usu = new Usuarios()
                {
                    IdUsuario = Guid.NewGuid().ToString(),
                    Identificacion = dto.Identificacion,
                    IdPerfil = dto.IdPerfil,
                    IdTienda = dto.IdTienda,
                    Nombre = dto.Nombre,
                    Apellido1 = dto.Apellido1,
                    Apellido2 = dto.Apellido2,
                    Contrasenna = dto.Contrasenna,
                    Correo = dto.Correo,
                    FechaCreacion = await _utilidades.ObtenerFecha(),
                    FechaActualiza = null,
                    UActualiza = null,
                };

                var repository = _unitOfWork.GetRepository<Usuarios>();
                repository.Add(usu);
                await _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return false;
            }
        }

        public async Task<UsuarioInicioSesionDto> ValidaUsuarioSesionAsync(string correoStr, string constrasenaStr)
        {
            try
            {
                var constrasena = await _utilidades.EncriptaString(constrasenaStr);
                var usuario = await _unitOfWork.GetRepository<Usuarios>().AllIncluding(ti => ti.Tiendas)
                                                                        .Where(x => x.Correo == correoStr && x.Contrasenna == constrasena)
                                                                        .FirstOrDefaultAsync();
                if (usuario == null) return null;

                var dto = new UsuarioInicioSesionDto()
                {
                    Id = usuario.IdUsuario,
                    Nombre = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
                    Perfil = await ObtienePerfilUsuario(usuario.IdPerfil),
                    IdTienda = usuario.IdTienda,
                    TiendaNombre = usuario.Tiendas.Nombre,
                    jwtToken = ""
                };

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar validacion de inicio de sesión");
            }
        }

        public async Task<PerfilDto> ObtienePerfilUsuario(string idPerfil)
        {
            try
            {
                var perfilResult = await _unitOfWork.GetRepository<Perfil>().All
                                                                        .Where(x => x.IdPerfil == idPerfil)
                                                                        .FirstAsync();

                var perfil = new PerfilDto()
                {
                    IdPerfil = perfilResult.IdPerfil,
                    Descripcion = perfilResult.Descripcion,
                };

                perfil.Permisos = new List<PermisosDto>();

                var perfilPermisosResult = await _unitOfWork.GetRepository<PerfilPermisos>().AllIncluding(per => per.Permiso)
                                                                        .Where(x => x.IdPerfil == idPerfil)
                                                                        .ToListAsync();

                foreach (var ln in perfilPermisosResult)
                {
                    perfil.Permisos.Add(new PermisosDto
                    {
                        IdPermiso = ln.IdPermiso,
                        Clave = ln.Permiso.Clave,
                        Descripcion = ln.Permiso.Descripcion
                    });
                }

                return perfil;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
