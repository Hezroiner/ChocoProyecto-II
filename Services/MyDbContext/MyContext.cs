using Microsoft.EntityFrameworkCore;
using Proyecto_II.Entities;

namespace Services.MyDbContext
{
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=MSI;Database=apitest;Trusted_Connection=True; MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<TipoCita> TiposCita { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Paciente y Cita
            modelBuilder.Entity<Cita>()
                .HasOne(cita => cita.Paciente)
                .WithMany(paciente => paciente.Citas)
                .HasForeignKey(cita => cita.PacienteId);

            //Cita y Sucursal
                modelBuilder.Entity<Cita>()
                .HasOne(cita => cita.Sucursal)
                .WithMany(sucursal => sucursal.Citas)
                .HasForeignKey(cita => cita.SucursalId);

            // User, Role y UserRole (relación muchos a muchos)
            modelBuilder.Entity<UserRole>()
                .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.User)
                .WithMany(user => user.UserRoles)
                .HasForeignKey(userRole => userRole.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.UserRoles)
                .HasForeignKey(userRole => userRole.RoleId);
        }
    }
}