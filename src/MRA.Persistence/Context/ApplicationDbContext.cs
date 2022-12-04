using Microsoft.EntityFrameworkCore;
using MRA.CrossCuttingConcerns.Identity;
using MRA.CrossCuttingConcerns.OS;
using MRA.Domain.Common;
using MRA.Domain.Entities;
using MRA.Persistence.Context.Seeds.Application;

namespace MRA.Persistence.Context
{
    public class ApplicationDbContext : AuditableContext
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ILoggedInUserService loggedInUserService,
            IDateTimeProvider dateTimeProvider)
           : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _loggedInUserService = loggedInUserService;
            _dateTimeProvider = dateTimeProvider;
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    property.SetColumnType("decimal(18,2)");
                }

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.ApplicationSeed();

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        entry.Entity.CreatedDate = _dateTimeProvider.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        entry.Entity.LastModifiedDate = _dateTimeProvider.UtcNow;
                        break;
                }
            }
            if (_loggedInUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_loggedInUserService.UserId);
            }
        }
    }
}
