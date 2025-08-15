using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRE.Models;
using MRE.Models.SearchObjects;

namespace MRE.Services
{
    public interface IMoviesService : IService<Movies, BaseSearchObject>
    {
        public Task<List<Movies>> GetNext10Movies(bool isShow = false, int take = 10, int skip=0, BaseSearchObject? search = null);
    }
}
