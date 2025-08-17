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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MRE.Services
{
    public class MoviesService : BaseService<Movies, Movie, MovieSearchObject>, IMoviesService
    {
        public MoviesService(MovieRatingEngineContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<Movies>> GetTop10(bool isShow = false) 
        {
            var query = _context.Movies.AsQueryable();

            query =query.Where(x => x.IsShow == isShow);

            query =
               query.Include(x => x.MovieRatings)
           .Include(x => x.MovieActors).ThenInclude(x => x.Actor);
    
            query=query.OrderByDescending(x=>x.AverageRate).Skip(0)
            .Take(10);

            var list= await query.ToListAsync();

            //foreach(var movie in list)
            //{
            //    movie.AverageRate = Math.Round((decimal)_context.MovieRatings.Where(x => x.MovieId == movie.MovieId).Select(x => x.Rate).Average(),2);
            //}

            var tmp = _mapper.Map<List<Movies>>(list);

            return tmp;

        }

        public override IQueryable<Movie> AddFilter(IQueryable<Movie> query, MovieSearchObject? search = null)
        {
            query = query.Where(x => x.IsShow == search.isShow);
            query = query.OrderByDescending(x => x.AverageRate);

            if (search?.FTS != null)
            {
                query = query.Where(x =>
                         x.Title.Contains(search.FTS) ||
                         x.Description.Contains(search.FTS) ||
                         x.MovieActors.Any(ma =>
                             ma.Actor.FirstName.Contains(search.FTS) ||
                             ma.Actor.LastName.Contains(search.FTS)
                         )
                     );
            }

            return base.AddFilter(query, search);
        }

        public override IQueryable<Movie> AddInclude(IQueryable<Movie> query, MovieSearchObject? search = null)
        {
            query = query.Include(x => x.MovieRatings);
            query = query.Include(x => x.MovieActors).ThenInclude(x=>x.Actor);
            return base.AddInclude(query, search);
        }


        public override async Task<Movie> AddIncludeForGetById(IQueryable<Movie> query, int id)
        {
            query = query.Include(x => x.MovieRatings);
            query = query.Include(x => x.MovieActors).ThenInclude(x => x.Actor); 
            var entity = await query.FirstOrDefaultAsync(x => x.MovieId == id);
            entity.AverageRate = Math.Round((decimal)_context.MovieRatings.Where(x => x.MovieId == id).Select(x => x.Rate).Average(),2);
            return entity;

        }

    }
}
