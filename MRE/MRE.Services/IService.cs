using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MRE.Models;
using MRE.Models.SearchObjects;

namespace MRE.Services
{
    public interface IService<T, TSearch> where T : class where TSearch : class
    {
        Task<PagedResult<T>> Get(TSearch search = null);
        Task<T> GetById(int id);
    }
}
