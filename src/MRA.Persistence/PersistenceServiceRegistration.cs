using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRA.Application.Contracts.Persistence;
using MRA.Persistence.Context;
using MRA.Persistence.Repositories;

namespace MRA.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MRAConnectionString"), builder => builder.EnableRetryOnFailure()));

            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepository<>));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            return services;
        }
    }
}
