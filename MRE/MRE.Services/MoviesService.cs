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
using OpenAI.Chat;

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

        public IQueryable<Movie> FilterExactStars(IQueryable<Movie> query, int stars)
        {
            query = query.Where(x => x.AverageRate == (decimal)stars);
            return query;
        }
        public IQueryable<Movie> FilterXStarsOrMore(IQueryable<Movie> query, int stars)
        {
            query = query.Where(x => x.AverageRate >= (decimal)stars);
            return query;
        }
        public IQueryable<Movie> FilterXStarsOrLess(IQueryable<Movie> query, int stars)
        {
            query = query.Where(x => x.AverageRate <= (decimal)stars);
            return query;
        }
        public IQueryable<Movie> FilterOlderThanXYears(IQueryable<Movie> query, int years)
        {
            var year = DateTime.Now.Year - years; 
            query = query.Where(x => x.ReleaseDate.Year <= year);
            return query;
        }
        public IQueryable<Movie> FilterAfterXYear(IQueryable<Movie> query, int year)
        {
            query = query.Where(x => x.ReleaseDate.Year > year);
            return query;
        }
        public IQueryable<Movie> FilterBeforeXYear(IQueryable<Movie> query, int year)
        {
            query = query.Where(x => x.ReleaseDate.Year < year);
            return query;
        }
        public IQueryable<Movie> FilterExactXYear(IQueryable<Movie> query, int year)
        {
            query = query.Where(x => x.ReleaseDate.Year == year);
            return query;
        }
        public IQueryable<Movie> FilterRecentMovies(IQueryable<Movie> query)
        {
            query = query.Where(x => x.ReleaseDate.Year == DateTime.Now.Year);
            //currently in the database there is no recent movies
            return query;
        }
        public IQueryable<Movie> FilterPopularMovies(IQueryable<Movie> query)
        {
            query = query.Where(x => x.AverageRate >=4);
            return query;
        }

        
        private static readonly Dictionary<string, (int Function, int Parameter)> _gptCache
            = new Dictionary<string, (int, int)>();


        public override IQueryable<Movie> BeforeFilter(IQueryable<Movie> query, MovieSearchObject? search = null)
        {
            query = query.Where(x => x.IsShow == search.isShow);
            query = query.OrderByDescending(x => x.AverageRate);
            return base.BeforeFilter(query, search);
        }

        public override IQueryable<Movie> AddFilter(IQueryable<Movie> query, MovieSearchObject? search = null)
        {
            if (!string.IsNullOrWhiteSpace(search?.FTS))
            {
                // Basic text search
                var q = query.Where(x =>
                    x.Title.Contains(search.FTS) ||
                    x.Description.Contains(search.FTS) ||
                    x.MovieActors.Any(ma =>
                        ma.Actor.FirstName.Contains(search.FTS) ||
                        ma.Actor.LastName.Contains(search.FTS)
                    )
                );

                if (q.Any())
                {
                    query = q;
                }
                else
                {
                    (int function, int parameter) funcResult;

                    // Check if we already cached this search term
                    if (_gptCache.TryGetValue(search.FTS, out funcResult))
                    {
                        Console.WriteLine($"[CACHE HIT] Using cached GPT result for: {search.FTS}");
                    }
                    else
                    {
                        // Ask GPT for filter mapping
                        ChatClient client = new(model: "gpt-4o", apiKey: Environment.GetEnvironmentVariable("API_KEY"));

                        ChatCompletion completion = client.CompleteChat($@"
                        Based on the user prompt '{search.FTS}', tell me which filter method is best.
                        Methods:
                          1 = exact X stars
                          2 = at least X stars
                          3 = at most X stars
                          4 = older than X years
                          5 = after year X
                          6 = before year X
                          7 = in exact year X
                          8 = recent movies
                          9 = popular or most watched movies
                        Your response should be in this format: 'function,parameter'. Example: '1,3'. If no match: '0,0'.");

                        var result = completion.Content[0].Text.Trim();
                        Console.WriteLine($"[ASSISTANT]: {result}");

                        var parts = result.Split(",");
                        if (parts.Length == 2 &&
                            int.TryParse(parts[0], out int function) &&
                            int.TryParse(parts[1], out int parameter))
                        {
                            funcResult = (function, parameter);
                            // Store in cache
                            _gptCache[search.FTS] = funcResult;
                        }
                        else
                        {
                            funcResult = (0, 0);
                        }
                    }

                    // Apply filter
                    switch (funcResult.function)
                    {
                        case 1: query = FilterExactStars(query, funcResult.parameter); break;
                        case 2: query = FilterXStarsOrMore(query, funcResult.parameter); break;
                        case 3: query = FilterXStarsOrLess(query, funcResult.parameter); break;
                        case 4: query = FilterOlderThanXYears(query, funcResult.parameter); break;
                        case 5: query = FilterAfterXYear(query, funcResult.parameter); break;
                        case 6: query = FilterBeforeXYear(query, funcResult.parameter); break;
                        case 7: query = FilterExactXYear(query, funcResult.parameter); break;
                        case 8: query = FilterRecentMovies(query); break;
                        case 9: query = FilterPopularMovies(query); break;
                        default: break; // 0,0 or unrecognized
                    }
                }
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
