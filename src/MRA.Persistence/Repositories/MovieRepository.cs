using MRA.Application.Contracts.Persistence;
using MRA.Domain.Entities;
using MRA.Persistence.Context;

namespace MRA.Persistence.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
