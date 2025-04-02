using Microsoft.EntityFrameworkCore;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }

        public DbSet<Autores> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Comentarios>().ToTable("comentarios_libros");
            modelBuilder.Entity<Autores>().ToTable("autores");
            modelBuilder.Entity<Libros>().ToTable("libros");
            modelBuilder.Entity<Clientes>().ToTable("clientes");
            modelBuilder.Entity<Categorias>().ToTable("categorias");
        }
    }
}