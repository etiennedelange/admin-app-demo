//using AdminApp.Data.Models;
//using System;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Diagnostics;

//namespace AdminApp.Data.NET
//{
//    /// <summary>
//    /// Useful scripts
//    /// --------------
//    /// EF Core - Generate idempotent script: entityframeworkcore\Script-Migration -i / entityframeworkcore\Script-Migration -Idempotent
//    /// EF6 - entityframework6\Update-Database -Script -SourceMigration: $InitialDatabase
//    /// Remove previous migration: entityframeworkcore\remove-migration -context adminappdbcontext
//    /// </summary>
//    public class AdminAppContext : DbContext
//    {
//        public AdminAppContext()
//            : this("DefaultConnection")
//        { }

//        public AdminAppContext(string connectionString)
//            : base(connectionString)
//        {
//            Database.Log = (logMessage) => Debug.WriteLine(logMessage);
//        }

//        public DbSet<Attorney> Attorney { get; set; }
//        public DbSet<Setting> Setting { get; set; }
//        public DbSet<AttorneySetting> AttorneySetting { get; set; }
//        public DbSet<AuditLog> AuditLog { get; set; }
//        public DbSet<Error> Error { get; set; }
//        public DbSet<Template> Template { get; set; }
//        public DbSet<DesktopProductVersion> DesktopProductVersion { get; set; }
//        public DbSet<OnlineProductVersion> OnlineProductVersion { get; set; }
//        public DbSet<Notification> Notification { get; set; }

//        protected override void OnModelCreating(DbModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.Conventions.Remove<PluralizingTableNameConvention>();
//            builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
//            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

//            builder.Properties<DateTime>().Configure(a => a.HasColumnType("datetime2"));

//            builder.Entity<Attorney>()
//                .HasIndex(a => a.Kref)
//                .IsUnique();

//            builder.Entity<Setting>()
//                .HasIndex(a => a.Guid)
//                .IsUnique();

//            builder.Entity<Template>()
//                .HasIndex(a => a.Guid)
//                .IsUnique();

//            builder.Entity<DesktopProductVersion>()
//                .HasIndex(a => a.VersionNumber)
//                .IsUnique();

//            builder.Entity<OnlineProductVersion>()
//                .HasIndex(a => a.VersionNumber)
//                .IsUnique();

//            builder.Entity<AttorneySetting>().
//                HasKey(a => new { a.AttorneyId, a.SettingId });

//            builder.Entity<Attorney>()
//                .HasMany(c => c.Settings)
//                .WithRequired()
//                .HasForeignKey(c => c.AttorneyId)
//                .WillCascadeOnDelete(true);

//            builder.Entity<Setting>()
//                .HasMany(c => c.Attorneys)
//                .WithRequired()
//                .HasForeignKey(c => c.SettingId);

//            builder.Entity<Template>()
//                .HasMany(a => a.DesktopProductVersions)
//                .WithMany(i => i.Templates)
//                .Map(ai =>
//                {
//                    ai.MapLeftKey("TemplateId");
//                    ai.MapRightKey("DesktopProductVersionId");
//                });

//            builder.Entity<Template>()
//                .HasMany(a => a.OnlineProductVersions)
//                .WithMany(i => i.Templates)
//                .Map(ai =>
//                {
//                    ai.MapLeftKey("TemplateId");
//                    ai.MapRightKey("OnlineProductVersionId");
//                });

//            builder.Entity<Template>()
//                .HasRequired(e => e.TemplateData)
//                .WithRequiredPrincipal();

//            builder.Entity<Template>().HasKey(x => x.Id);
//            builder.Entity<TemplateData>().HasKey(x => x.Id);

//            builder.Entity<Template>().ToTable("Template");
//            builder.Entity<TemplateData>().ToTable("Template");
//        }
//    }
//}

