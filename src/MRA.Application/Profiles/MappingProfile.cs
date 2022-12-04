using AutoMapper;
using MRA.Application.Features.Movies.Queries.GetMovieDetail;
using MRA.Application.Features.Movies.Queries.GetMoviesList;
using MRA.Domain.Entities;

namespace MRA.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // For Movies
            CreateMap<Movie, MovieListVm>()
                .ForMember(dest =>
                        dest.MovieId,
                        opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Movie, MovieDetailVm>()
                 .ForMember(dest =>
                        dest.MovieId,
                        opt => opt.MapFrom(src => src.Id))
                 .ReverseMap();
        }
    }
}
