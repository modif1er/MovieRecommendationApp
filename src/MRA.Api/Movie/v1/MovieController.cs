using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRA.Api.Controllers;
using MRA.Application.Features.Movies.Queries.GetMovieDetail;
using MRA.Application.Features.Movies.Queries.GetMoviesList;

namespace MRA.Api.Movie.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        [HttpGet("all", Name = "GetAllMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MovieListVm>>> GetAllMovies()
        {
            var dtos = await Mediator.Send(new GetMoviesListQuery());
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var getMovieDetailQuery = new GetMovieDetailQuery() { Id = id };
            return Ok(await Mediator.Send(getMovieDetailQuery));
        }
    }
}
