using MRA.Domain.Entities;

namespace MRA.Application.Contracts.Persistence
{
    public interface IMovieRepository : IGenericRepositoryAsync<Movie>
    {
    }
}
