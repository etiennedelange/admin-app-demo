using AdminApp.Data.Models;
using AdminApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Data
{
    public class AdminAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public AdminAppDbContext(DbContextOptions options)
            : base(options)
        { }

        public AdminAppDbContext()
            : base()
        { }

        public DbSet<Attorney> Attorneys { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<AttorneySetting> AttorneySettings { get; set; }
        public DbSet<LoggedEvent> LoggedEvents { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Error> Errors { get; set; }

        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder
            //    .UseLoggerFactory(MyLoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
            //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFLogging;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Attorney>().ToTable("Attorney").HasIndex(a => a.Kref).IsUnique();
            builder.Entity<Setting>().ToTable("Setting").HasIndex(a => a.Guid).IsUnique();
            builder.Entity<AttorneySetting>().ToTable("AttorneySetting");
            builder.Entity<Error>().ToTable("Error");
            builder.Entity<JobTitle>().ToTable("JobTitle");
            builder.Entity<LoggedEvent>().ToTable("LoggedEvent");

            builder.Entity<AttorneySetting>().
                HasKey(a => new { a.AttorneyID, a.SettingID });

            builder.Entity<AttorneySetting>()
                .HasOne(x => x.Attorney)
                .WithMany(y => y.AttorneySettings)
                .HasForeignKey(y => y.AttorneyID);

            builder.Entity<AttorneySetting>()
                .HasOne(x => x.Setting)
                .WithMany(y => y.AttorneySettings)
                .HasForeignKey(y => y.SettingID);
        }
    }
}

