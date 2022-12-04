using AutoMapper;
using MediatR;
using MRA.Application.Contracts.Persistence;
using MRA.Domain.Entities;

namespace MRA.Application.Features.Movies.Queries.GetMoviesList
{
    public class GetMoviesListQueryHandler : IRequestHandler<GetMoviesListQuery, List<MovieListVm>>
    {
        private readonly IGenericRepositoryAsync<Movie> _movieRepository;
        private readonly IMapper _mapper;

        public GetMoviesListQueryHandler(IMapper mapper, IGenericRepositoryAsync<Movie> movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieListVm>> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
        {
            var allEvents = (await _movieRepository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<MovieListVm>>(allEvents);
        }
    }
}
