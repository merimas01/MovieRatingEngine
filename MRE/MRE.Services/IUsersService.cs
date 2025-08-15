using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRE.Models;
using MRE.Models.Requests;
using MRE.Models.SearchObjects;

namespace MRE.Services
{
    public interface IUsersService : ICRUDService<Users, BaseSearchObject, UserInsertRequest>
    {
        public Task<Users> Login(string username, string password);
    }
}
