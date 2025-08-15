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

        [HttpGet("next10/{isShow}/{take}/{skip}")]
        public async Task<List<Movies>> GetNext10Movies(bool isShow = false, int take = 10, int skip=0, [FromQuery] BaseSearchObject? search = null)
        {
            return await _service.GetNext10Movies(isShow, take, skip, search);   
        }
    }
}
