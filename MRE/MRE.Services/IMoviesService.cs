using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRE.Models;
using MRE.Models.SearchObjects;

namespace MRE.Services
{
    public interface IMoviesService : IService<Movies, MovieSearchObject>
    {
        public Task<List<Movies>> GetTop10(bool isShow = false);
    }
}
