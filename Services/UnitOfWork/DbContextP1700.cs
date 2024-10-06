using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Services.UnitOfWork
{
    public class DbContextP1700 : DbContext
    {
        public DbContextP1700(DbContextOptions<DbContextP1700> options) : base(options)
        {
        }

        protected DbContextP1700()
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Tiendas> Tiendas { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<PerfilPermisos> PerfilPermisos { get; set; }
    }
}
