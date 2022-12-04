using MediatR;

namespace MRA.Application.Features.Movies.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery : IRequest<MovieDetailVm>
    {
        public int Id { get; set; }
    }
}
