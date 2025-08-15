using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRE.Models;
using MRE.Models.Requests;
using MRE.Models.SearchObjects;
using MRE.Services;
using MRE.Services.Database;

namespace MRE.Controllers
{
    [ApiController]
    public class MovieRatingsController : BaseCRUDController<Models.MovieRatings, BaseSearchObject, MovieRatingsInsertRequest>
    {
        IMovieRatingsService _service;
        public MovieRatingsController(ILogger<BaseCRUDController<Models.MovieRatings, BaseSearchObject, MovieRatingsInsertRequest>> logger, IMovieRatingsService service)
            : base(logger, service)
        {
            _service = service;
        }

        [Authorize]
        public override Task<MovieRatings> Insert([FromBody] MovieRatingsInsertRequest insert)
        {
            return base.Insert(insert);
        }

    }
}
