namespace MRA.Application.Features.Movies.Queries.GetMovieDetail
{
    public class MovieDetailVm
    {
        public int MovieId { get; set; }
        public string Name { get; set; }

        public int GenreId { get; set; }
        public GenreDto Genre { get; set; }
    }
}
