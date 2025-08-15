using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MRE.Models;
using MRE.Models.Requests;
using MRE.Services.Database;

namespace MRE.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, Users>();
            CreateMap<UserInsertRequest, User>();

            CreateMap<MovieRating, MovieRatings>();
            CreateMap<MovieRatingsInsertRequest, MovieRating>();

            CreateMap<Movie, Movies>();
            CreateMap<Actor, Actors>();
            CreateMap<MovieActor, MovieActors>();


        }
    }
}
