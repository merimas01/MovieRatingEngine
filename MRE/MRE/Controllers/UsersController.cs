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
    public class UsersController : BaseCRUDController<Models.Users, BaseSearchObject, UserInsertRequest>
    {
        IUsersService _service;
        public UsersController(ILogger<BaseCRUDController<Models.Users, BaseSearchObject, UserInsertRequest>> logger,  IUsersService service)
            : base(logger, service)
        {
            _service = service;
        }

        [Authorize]
        public override Task<Users> Insert([FromBody] UserInsertRequest insert)
        {
            return base.Insert(insert);
        }

    }
}
