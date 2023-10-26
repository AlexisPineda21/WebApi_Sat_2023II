using Microsoft.EntityFrameworkCore;
using WebAPI_Sat_2023II.git.DAL.Entities;

namespace WebAPI_Sat_2023II.git.DAL
{
    public class DataBaseContext : DbContext
    {
        //Con este constructor me podré conectar a la DB.
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); //Esto es un indice para evitar nombres duplicados de países.
        }

        public DbSet<Country> Countries { get; set; } //Mapea la clase en SqlServer en este caso country.

    }
}
