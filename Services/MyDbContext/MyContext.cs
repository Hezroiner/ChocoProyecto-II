using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;

namespace Services.MyDbContext
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<TipoCita> TiposCita { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User y Cita
            modelBuilder.Entity<Cita>()
                .HasOne(cita => cita.User)
                .WithMany(user => user.Citas)
                .HasForeignKey(cita => cita.UserId);

            //Cita y Sucursal
            modelBuilder.Entity<Cita>()
                .HasOne(cita => cita.Sucursal)
                .WithMany(sucursal => sucursal.Citas)
                .HasForeignKey(cita => cita.SucursalId);

            //Relación uno a muchos entre User y Role
            modelBuilder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId);

  
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Nombre = "ADMIN"},
                new Role { RoleId = 2, Nombre = "USER" });

            modelBuilder.Entity<Sucursal>().HasData(
                new Sucursal { SucursalId = 1, Nombre = "Clinica Santa Cruz" },
                new Sucursal { SucursalId = 2, Nombre = "Clinica Nicoya" },
                new Sucursal { SucursalId = 3, Nombre = "Clinica Libera" });
            
            //Creando los datos de TipoCita
            modelBuilder.Entity<TipoCita>().HasData(
                new TipoCita { TipoCitaId = 1, Nombre = "Medicina General" },
                new TipoCita { TipoCitaId = 2, Nombre = "Odontología" },
                new TipoCita { TipoCitaId = 3, Nombre = "Pediatría" },
                new TipoCita { TipoCitaId = 4, Nombre = "Neurología" });
        }
    }
}