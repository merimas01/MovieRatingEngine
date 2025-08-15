
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRE.Services;
using MRE.Models;

namespace MRE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCRUDController<T, TSearch, TInsert> : BaseController<T, TSearch> where T : class where TSearch : class
    {
        protected readonly ICRUDService<T, TSearch, TInsert> _service;
        protected readonly ILogger<BaseCRUDController<T, TSearch, TInsert>> _logger;

        public BaseCRUDController(ILogger<BaseCRUDController<T, TSearch, TInsert >> logger, ICRUDService<T, TSearch, TInsert> service) : base(logger, service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public virtual async Task<T> Insert([FromBody] TInsert insert)
        {
            return await _service.Insert(insert);
        }

    }
}
