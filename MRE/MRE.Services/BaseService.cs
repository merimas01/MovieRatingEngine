using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MRE.Models.SearchObjects;
using MRE.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace MRE.Services
{
    public class BaseService<T, TDb, TSearch> : IService<T, TSearch> where TDb : class where T : class where TSearch : BaseSearchObject
    {
        protected MovieRatingEngineContext _context;
        protected IMapper _mapper { get; set; }

        public BaseService(MovieRatingEngineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<List<T>> Get(TSearch search)
        {
            var query = _context.Set<TDb>().AsQueryable();

            List<T> result = new List<T>();

            query = AddInclude(query, search);

            query = AddFilter(query, search);

            var list = await query.ToListAsync();

            var tmp = _mapper.Map<List<T>>(list);

            result = tmp;

            return result;
        }

        public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch? search = null)
        {
            return query;
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch? search = null)
        {
            return query;
        }


        public virtual async Task<TDb> AddIncludeForGetById(IQueryable<TDb> query, int id)
        {
            return (TDb)query;
        }

        public virtual async Task<T> GetById(int id)
        {
            var query = _context.Set<TDb>().AsQueryable();

            var entity = await AddIncludeForGetById(query, id);

            return _mapper.Map<T>(entity);
        }

    }
}
