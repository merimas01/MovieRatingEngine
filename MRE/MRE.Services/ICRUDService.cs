using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRE.Models.SearchObjects;

namespace MRE.Services
{
    public interface ICRUDService<T, TSearch, TInsert> : IService<T, TSearch> where T : class where TSearch : class
    {
        Task<T> Insert(TInsert request);
       
    }
}
