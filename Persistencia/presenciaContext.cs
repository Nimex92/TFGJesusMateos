using ClassLibray;
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

        public DbSet<Signing> Signings { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<SignedWorker> SignedWorkers { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }
        public DbSet<StartedTask> StartedTasks { get; set; }
        public DbSet<EndedTask> EndedTasks { get; set; }
        public DbSet<Places> Places { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Day> DayOff { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<Payroll> WorkerPayrolls { get; set; }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseMySql("server=localhost;port=3306;uid=root;pwd='';database=tfgJesusMateos;", new MySqlServerVersion(new Version(8, 0, 28)));
        }
    }
    public class BloggingContextFactory : IDesignTimeDbContextFactory<PresenciaContext>
    {
        public PresenciaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PresenciaContext>();
            optionsBuilder.UseMySql("server=localhost;port=3306;uid=root;pwd='';database=tfgJesusMateos;", new MySqlServerVersion((new Version(8, 0, 28))));
            return new PresenciaContext(optionsBuilder.Options);
        }
    }
}