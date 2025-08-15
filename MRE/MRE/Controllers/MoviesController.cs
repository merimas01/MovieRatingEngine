using Microsoft.AspNetCore.Mvc;
using MRE.Controllers;
using MRE.Models;
using MRE.Models.SearchObjects;
using MRE.Services;

namespace MRE.Controllers
{
    public class MoviesController : BaseController<Models.Movies, BaseSearchObject>
    {
        IMoviesService _service;
        public MoviesController(ILogger<BaseController<Movies, BaseSearchObject>> logger, IMoviesService service) : base(logger, service)
        {
            _service = service;
        }

        [HttpGet("top10/{isShow}/{take}")]
        public async Task<List<Movies>> GetTop10Movies(bool isShow = false, int take = 10, [FromQuery] BaseSearchObject? search = null)
        {
            return await _service.GetTop10Movies(isShow, take, search);   
        }
    }
}
