using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MRE.Models;
using MRE.Models.SearchObjects;
using MRE.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace MRE.Services
{
    public class MoviesService : BaseService<Movies, Movie, BaseSearchObject>, IMoviesService
    {
        public MoviesService(MovieRatingEngineContext context, IMapper mapper) : base(context, mapper)
        {
        }
      


        public async Task<List<Movies>> GetTop10Movies(bool isShow = false, int take=10, BaseSearchObject? search=null) //length + 10
        {

            var query = _context.Movies.AsQueryable();

            if (isShow == false) {
                query.Where(x => x.IsShow == false);
            } else query.Where(x => x.IsShow == true);



            query =
               query.Include(x => x.MovieRatings)
           .Include(x => x.MovieActors).ThenInclude(x => x.Actor);


            if(search?.FTS!=null && search?.FTS.Length >= 2) {
                query = query.Where(x => x.Title.Contains(search.FTS) || x.Description.Contains(search.FTS));         
            }

            query =query.OrderByDescending(x => x.MovieRatings.Average(r => r.Rate))
            .Take(take);

            var list= await query.ToListAsync();

            var tmp = _mapper.Map<List<Movies>>(list);

            return tmp;

        }


        public override IQueryable<Movie> AddInclude(IQueryable<Movie> query, BaseSearchObject? search = null)
        {
            query = query.Include(x => x.MovieRatings);
            query = query.Include(x => x.MovieActors);
            return base.AddInclude(query, search);
        }


        public override async Task<Movie> AddIncludeForGetById(IQueryable<Movie> query, int id)
        {
            query = query.Include(x => x.MovieRatings);
            query = query.Include(x => x.MovieActors);
            var entity = await query.FirstOrDefaultAsync(x => x.MovieId == id);
            entity.AverageRate = (decimal)_context.MovieRatings.Where(x => x.MovieId == id).Select(x => x.Rate).Average();
            return entity;

        }

    }
}
