using Microsoft.EntityFrameworkCore;

namespace L02P02_2022HM651_2022DP650.Models
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }


    }
}
