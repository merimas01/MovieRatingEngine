using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        
    }
}
