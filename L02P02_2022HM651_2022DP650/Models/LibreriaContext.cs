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


    }
}
