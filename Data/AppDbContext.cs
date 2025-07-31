using CadastroEmpresa.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroEmpresa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EmpresaModel> Empresas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UsuarioModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<EmpresaModel>()
                .HasIndex(e => e.Cnpj)
                .IsUnique();
        }
    }
}

