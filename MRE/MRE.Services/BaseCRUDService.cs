using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MRE.Models.SearchObjects;
using MRE.Services.Database;

namespace MRE.Services
{

    public class BaseCRUDService<T, TDb, TSearch, TInsert> : BaseService<T, TDb, TSearch> where T : class where TDb : class where TSearch : BaseSearchObject
    {
        public BaseCRUDService(MovieRatingEngineContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public virtual async Task BeforeInsert(TDb entity, TInsert insert)
        {

        }

        public virtual async Task AfterInsert(TDb entity, TInsert insert)
        {

        }

        

        public virtual async Task<bool> AddValidationInsert(TInsert request)
        {
            return true;
        }

      

        public virtual async Task<T> Insert(TInsert insert)
        {
            var set = _context.Set<TDb>();

            TDb entity = _mapper.Map<TDb>(insert);

            await BeforeInsert(entity, insert);
            var validate = await AddValidationInsert(insert);

            if (validate == true)
            {
                set.Add(entity);

                await _context.SaveChangesAsync();

                await AfterInsert(entity, insert);

                await _context.SaveChangesAsync();

                return _mapper.Map<T>(entity);
            }
            else throw new Exception("error");
        }


    }
}
