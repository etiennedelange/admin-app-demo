using System.Threading;
using System.Threading.Tasks;
using AdminApp.Common.Services.Interfaces;
using AdminApp.Data.Core;
using AdminApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Data
{
	public class AdminAppContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
	{
		private readonly IAuditLoggingService _auditLoggingService;

		//public AdminAppContext(DbContextOptions<AdminAppContext> options, IAuditLoggingService auditLoggerService)
		//	: base(options)
		//{
		//	_auditLoggingService = auditLoggerService;
		//}

		//public AdminAppContext(IAuditLoggingService auditLoggerService)
		//	: base()
		//{
		//	_auditLoggingService = auditLoggerService;

		//	//Database.Log = (logMessage) => Debug.WriteLine(logMessage);
		//}

		public AdminAppContext(DbContextOptions<AdminAppContext> options)
			: base(options)
		{
			//_auditLoggingService = auditLoggerService;
		}

		public AdminAppContext()
			: base()
		{
			//_auditLoggingService = auditLoggerService;

			//Database.Log = (logMessage) => Debug.WriteLine(logMessage);
		}

		/// Changes for AdminAppContext will always be audited. In order to have more fine-grained control over this,
		/// any user changes will have to be done manually and SaveChangesAsync would have to be called as opposed to using the
		/// UserManager and RoleManager classes. Changes made with the UserManager and RoleManager classes automatically calls
		/// SaveChangesAsync
		public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			EntityChangeSerializerCore entityChangeSerializer = new(this);

			//await _auditLoggingService.LogAsync(entityChangeSerializer.GetChangesAsJson(new StringEnumConverter()));

			return await base.SaveChangesAsync(cancellationToken);
		}

		public DbSet<Attorney> Attorney { get; set; }
		public DbSet<Setting> Setting { get; set; }
		public DbSet<AttorneySetting> AttorneySetting { get; set; }
		public DbSet<AuditLog> AuditLog { get; set; }
		public DbSet<Error> Error { get; set; }
		public DbSet<Template> Template { get; set; }
		public DbSet<DesktopProductVersion> DesktopProductVersion { get; set; }
		public DbSet<OnlineProductVersion> OnlineProductVersion { get; set; }
		public DbSet<Notification> Notification { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//builder.Conventions.Remove<PluralizingTableNameConvention>();
			//builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			//builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			//builder.Properties<DateTime>().Configure(a => a.HasColumnType("datetime2"));

			builder.Entity<Attorney>()
				.HasIndex(a => a.Kref)
				.IsUnique();

			builder.Entity<Setting>()
				.HasIndex(a => a.Guid)
				.IsUnique();

			builder.Entity<Template>()
				.HasIndex(a => a.Guid)
				.IsUnique();

			builder.Entity<DesktopProductVersion>()
				.HasIndex(a => a.VersionNumber)
				.IsUnique();

			builder.Entity<OnlineProductVersion>()
				.HasIndex(a => a.VersionNumber)
				.IsUnique();

			builder.Entity<AttorneySetting>().
				HasKey(a => new { a.AttorneyId, a.SettingId });

			builder.Entity<AttorneySetting>()
				.HasOne(bc => bc.Attorney)
				.WithMany(b => b.Settings)
				.HasForeignKey(bc => bc.AttorneyId);

			builder.Entity<AttorneySetting>()
				.HasOne(bc => bc.Setting)
				.WithMany(c => c.Attorneys)
				.HasForeignKey(bc => bc.SettingId);

			//builder.Entity<Attorney>()
			//    .HasMany(c => c.Settings)
			//    .WithRequired()
			//    .HasForeignKey(c => c.AttorneyId)
			//    .WillCascadeOnDelete(true);

			//builder.Entity<Setting>()
			//    .HasMany(c => c.Attorneys)
			//    .WithRequired()
			//    .HasForeignKey(c => c.SettingId);

			//builder.Entity<Template>()
			//    .HasMany(a => a.DesktopProductVersions)
			//    .WithMany(i => i.Templates)
			//    .Map(ai =>
			//    {
			//        ai.MapLeftKey("TemplateId");
			//        ai.MapRightKey("DesktopProductVersionId");
			//    });

			//builder 

			//builder.Entity<Template>()
			//    .HasRequired(e => e.TemplateData)
			//    .WithRequiredPrincipal();

			//builder.Entity<Template>().HasKey(x => x.Id);
			//builder.Entity<TemplateData>().HasKey(x => x.Id);

			//builder.Entity<Template>().ToTable("Template");
			//builder.Entity<TemplateData>().ToTable("Template");
		}
	}
}