using Bibliotec;
using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistencia
{
    public class PresenciaContext : DbContext
    {
        public PresenciaContext() : base()
        {

        }
        public PresenciaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Fichajes> TablaFichajes { get; set; }
        public DbSet<Trabajador> Trabajador { get; set; }
        public DbSet<Grupo_Trabajo> Grupo_Trabajo { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseMySql("server=localhost;port=3306;uid=root;pwd='';database=pruebas;", new MySqlServerVersion(new Version(8, 0, 28)));
        }
    }
    public class BloggingContextFactory : IDesignTimeDbContextFactory<PresenciaContext>
    {
        public PresenciaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PresenciaContext>();
            optionsBuilder.UseMySql("server=localhost;port=3306;uid=root;pwd='';database=pruebas;", new MySqlServerVersion((new Version(8, 0, 28))));

            return new PresenciaContext(optionsBuilder.Options);
        }
    }
}