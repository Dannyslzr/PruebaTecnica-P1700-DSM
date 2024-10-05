using Models.Dtos.Usuario;
using Models.Entities;
using Services.Interfaces;
using Services.UnitOfWork;
using System.Drawing;

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

                if (string.IsNullOrEmpty(dto.Contrasenna)) dto.Contrasenna = constrasena;

                var usu = new Usuarios()
                {
                    IdUsuario = Guid.NewGuid().ToString(),

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
    }
}
