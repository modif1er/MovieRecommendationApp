using AutoMapper;
using MediatR;
using MRA.Application.Contracts.Persistence;
using MRA.Application.Exceptions;
using MRA.Domain.Entities;

namespace MRA.Application.Features.Movies.Queries.GetMovieDetail
{
    internal class GetMovieDetailQueryHandler : IRequestHandler<GetMovieDetailQuery, MovieDetailVm>
    {
        private readonly IGenericRepositoryAsync<Movie> _movieRepository;
        private readonly IGenericRepositoryAsync<Genre> _genreRepository;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryHandler(IMapper mapper,
            IGenericRepositoryAsync<Movie> movieRepository,
            IGenericRepositoryAsync<Genre> genreRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        public async Task<MovieDetailVm> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
        {
            var @movie = await _movieRepository.GetByIdAsync(request.Id);
            var movieDetailDto = _mapper.Map<MovieDetailVm>(@movie);

            var genre = await _genreRepository.GetByIdAsync(@movie.GenreId);

            if (genre == null)
            {
                throw new NotFoundException(nameof(Movie), request.Id);
            }
            movieDetailDto.Genre = _mapper.Map<GenreDto>(genre);

            return movieDetailDto;
        }
    }
}
