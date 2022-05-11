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

        public DbSet<Fichajes> Fichajes { get; set; }
        public DbSet<Trabajador> Trabajador { get; set; }
        public DbSet<TrabajadorEnTurno> TrabajadorEnTurno { get; set; } 
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        public DbSet<TareaComenzada> TareasComenzadas { get; set; }
        public DbSet<TareaFinalizada> TareasFinalizadas { get; set; }
        public DbSet<Zonas> Zonas { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Calendario> Calendario { get; set; }
        public DbSet<Dia> DiaLibre { get; set; }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<EquipoTrabajo> EquipoTrabajo { get; set; }
        public DbSet<SolicitudVacaciones> SolicitudesVacaciones { get; set; }
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