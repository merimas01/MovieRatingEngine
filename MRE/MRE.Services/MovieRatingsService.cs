using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MRE.Models;
using MRE.Models.Requests;
using MRE.Models.SearchObjects;
using MRE.Services.Database;

namespace MRE.Services
{
    public class MovieRatingsService : BaseCRUDService<MovieRatings, MovieRating, BaseSearchObject, MovieRatingsInsertRequest>, IMovieRatingsService
    {
        public MovieRatingsService(MovieRatingEngineContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task BeforeInsert(MovieRating entity, MovieRatingsInsertRequest insert)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => entity.MovieId == x.MovieId);
            if (movie != null)
            {
                movie.AverageRate = Math.Round((decimal)_context.MovieRatings.Where(x => x.MovieId == movie.MovieId).Select(x => x.Rate).Average(), 2);
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
