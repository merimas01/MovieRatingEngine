using Microsoft.AspNetCore.Mvc;
using MRE.Controllers;
using MRE.Models;
using MRE.Models.SearchObjects;
using MRE.Services;

namespace MRE.Controllers
{
    public class MoviesController : BaseController<Models.Movies, MovieSearchObject>
    {
        IMoviesService _service;
        public MoviesController(ILogger<BaseController<Movies, MovieSearchObject>> logger, IMoviesService service) : base(logger, service)
        {
            _service = service;
        }

        //[HttpGet("top10/{isShow}")]
        //public async Task<List<Movies>> GetTop10(bool isShow = false)
        //{
        //    return await _service.GetTop10(isShow);   
        //}
    }
}
