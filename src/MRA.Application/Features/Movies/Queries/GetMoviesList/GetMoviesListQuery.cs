using MediatR;

namespace MRA.Application.Features.Movies.Queries.GetMoviesList
{
    public class GetMoviesListQuery : IRequest<List<MovieListVm>>
    {
    }
}
