using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MRE;
using MRE.Services;
using MRE.Services.Database;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IMoviesService, MoviesService>();
builder.Services.AddTransient<IMovieRatingsService, MovieRatingsService>();
builder.Services.AddTransient<IUsersService, UsersService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basicAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "basic"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme, Id="basicAuth"}
                },
                new string[]{}
            }
        });
}
);


builder.Services.AddAutoMapper(typeof(IMoviesService));


builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieRatingEngineContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<MovieRatingEngineContext>();

    var con = dataContext.Database.GetConnectionString();
    Console.WriteLine(con);

    if (dataContext.Database.EnsureCreated())
    {
        dataContext.Database.Migrate();

        string password = "test"; // Same password for demonstration

        string salt1 = GenerateSalt();
        string hash1 = GenerateHash(salt1, password);

        dataContext.Users.AddRange(
          new User
          {
              FirstName= "Test",
              LastName= "Test",
              Username="test",
              PasswordHash= hash1,
              PasswordSalt= salt1,
              Role="Admin"
          }
            );

        dataContext.SaveChanges();

        var actor1 = new Actor { FirstName = "Leonardo", LastName = "DiCaprio" };
        var actor2 = new Actor { FirstName = "Kate", LastName = "Winslet" };
        var actor3 = new Actor { FirstName = "Morgan", LastName = "Freeman" };
        var actor4 = new Actor { FirstName = "Brad", LastName = "Pitt" };
        var actor5 = new Actor { FirstName = "Natalie", LastName = "Portman" };
        var actor6 = new Actor { FirstName = "Keanu", LastName = "Reeves" };
        var actor7 = new Actor { FirstName = "Tom", LastName = "Hanks" };
        var actor8 = new Actor { FirstName = "Emma", LastName = "Stone" };
        var actor9 = new Actor { FirstName = "Robert", LastName = "Downey Jr." };
        var actor10 = new Actor { FirstName = "Scarlett", LastName = "Johansson" };
        var actor11 = new Actor { FirstName = "Denzel", LastName = "Washington" };
        var actor12 = new Actor { FirstName = "Chris", LastName = "Hemsworth" };
        var actor13 = new Actor { FirstName = "Gal", LastName = "Gadot" };
        var actor14 = new Actor { FirstName = "Hugh", LastName = "Jackman" };
        var actor15 = new Actor { FirstName = "Anne", LastName = "Hathaway" };
        var actor16 = new Actor { FirstName = "Johnny", LastName = "Depp" };
        var actor17 = new Actor { FirstName = "Christian", LastName = "Bale" };
        var actor18 = new Actor { FirstName = "Margot", LastName = "Robbie" };
        var actor19 = new Actor { FirstName = "Matt", LastName = "Damon" };
        var actor20 = new Actor { FirstName = "Jennifer", LastName = "Lawrence" };

        var showActor1 = new Actor { FirstName = "Bryan", LastName = "Cranston" };
        var showActor2 = new Actor { FirstName = "Aaron", LastName = "Paul" };
        var showActor3 = new Actor { FirstName = "Emilia", LastName = "Clarke" };
        var showActor4 = new Actor { FirstName = "Kit", LastName = "Harington" };
        var showActor5 = new Actor { FirstName = "Peter", LastName = "Dinklage" };
        var showActor6 = new Actor { FirstName = "Maisie", LastName = "Williams" };
        var showActor7 = new Actor { FirstName = "Millie", LastName = "Bobby Brown" };
        var showActor8 = new Actor { FirstName = "David", LastName = "Schwimmer" };
        var showActor9 = new Actor { FirstName = "Jennifer", LastName = "Aniston" };
        var showActor10 = new Actor { FirstName = "Matthew", LastName = "Perry" };
        var showActor11 = new Actor { FirstName = "Steve", LastName = "Carell" };
        var showActor12 = new Actor { FirstName = "John", LastName = "Krasinski" };
        var showActor13 = new Actor { FirstName = "Kerry", LastName = "Washington" };
        var showActor14 = new Actor { FirstName = "Viola", LastName = "Davis" };
        var showActor15 = new Actor { FirstName = "Kitty", LastName = "Snyder" }; 
        var showActor16 = new Actor { FirstName = "Pedro", LastName = "Pascal" };
        var showActor17 = new Actor { FirstName = "Gillian", LastName = "Anderson" };
        var showActor18 = new Actor { FirstName = "David", LastName = "Duchovny" };
        var showActor19 = new Actor { FirstName = "Steve", LastName = "Buscemi" };
        var showActor20 = new Actor { FirstName = "James", LastName = "Gandolfini" };


        dataContext.Actors.AddRange(
          actor1, actor2, actor3, actor4, actor5,
          actor6, actor7, actor8, actor9, actor10,
          actor11, actor12, actor13, actor14, actor15,
          actor16, actor17, actor18, actor19, actor20,

          showActor1, showActor2, showActor3, showActor4, showActor5,
          showActor6, showActor7, showActor8, showActor9, showActor10,
          showActor11, showActor12, showActor13, showActor14, showActor15,
          showActor16, showActor17, showActor18, showActor19, showActor20
        );

        dataContext.SaveChanges();


        string filePath = @"C:\Users\Korisnik\Desktop\diplomski\logo\fit.jpg";
        byte[] imageBytes= File.ReadAllBytes( filePath );

        string path1 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\blackswan.jpg";
        byte[] blackswan = File.ReadAllBytes( path1 );

        string path2 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\fightclub.jpg";
        byte[] fightclub = File.ReadAllBytes(path2);

        string path3 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\forestgump.jpg";
        byte[] forestgump = File.ReadAllBytes(path3);

        string path4 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\inception.jpg";
        byte[] inception = File.ReadAllBytes(path4);

        string path5 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\lalaland.jpg";
        byte[] lalaland = File.ReadAllBytes(path5);

        string path6 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\shawshank.jpg";
        byte[] shawshank = File.ReadAllBytes(path6);

        string path7 = @"C:\Users\Korisnik\Desktop\MovieRatingEngine\MovieRatingEngine\MRE\MRE\images\titanic.png";
        byte[] titanic = File.ReadAllBytes(path7);

        string showFilePath = @"C:\Users\Korisnik\Desktop\diplomski\logo\fit.jpg";
        byte[] showImageBytes = File.ReadAllBytes(showFilePath);

        var movie1 = new Movie { Title = "Titanic", CoverImage = titanic, Description = "A romance on the doomed RMS Titanic.", IsShow = false, ReleaseDate = new DateTime(1997, 12, 19), AverageRate = 5 };
        var movie2 = new Movie { Title = "The Shawshank Redemption", CoverImage = shawshank, Description = "Two imprisoned men bond over years.", IsShow = false, ReleaseDate = new DateTime(1994, 9, 23), AverageRate = 5 };
        var movie3 = new Movie { Title = "Fight Club", CoverImage = fightclub, Description = "An underground fight club becomes more.", IsShow = false, ReleaseDate = new DateTime(1999, 10, 15), AverageRate = 4 };
        var movie4 = new Movie { Title = "Black Swan", CoverImage = blackswan, Description = "A dancer loses herself in the role.", IsShow = false, ReleaseDate = new DateTime(2010, 12, 3), AverageRate = 4 };
        var movie5 = new Movie { Title = "The Matrix", CoverImage = imageBytes, Description = "A hacker learns the truth about reality.", IsShow = false, ReleaseDate = new DateTime(1999, 3, 31), AverageRate = 5 };
        var movie6 = new Movie { Title = "Forrest Gump", CoverImage = forestgump, Description = "A slow-witted man experiences history.", IsShow = false, ReleaseDate = new DateTime(1994, 7, 6), AverageRate = 5 };
        var movie7 = new Movie { Title = "La La Land", CoverImage = lalaland, Description = "A romance between a jazz pianist and an actress.", IsShow = false, ReleaseDate = new DateTime(2016, 12, 9), AverageRate = 4 };
        var movie8 = new Movie { Title = "Iron Man", CoverImage = imageBytes, Description = "A billionaire becomes a superhero.", IsShow = false, ReleaseDate = new DateTime(2008, 5, 2), AverageRate = 4 };
        var movie9 = new Movie { Title = "Avengers: Endgame", CoverImage = imageBytes, Description = "The Avengers fight to undo Thanos' actions.", IsShow = false, ReleaseDate = new DateTime(2019, 4, 26), AverageRate = 5 };
        var movie10 = new Movie { Title = "Training Day", CoverImage = imageBytes, Description = "A rookie cop's first day with a corrupt detective.", IsShow = false, ReleaseDate = new DateTime(2001, 10, 5), AverageRate = 4 };
        var movie11 = new Movie { Title = "Thor: Ragnarok", CoverImage = imageBytes, Description = "Thor fights to save Asgard from Hela.", IsShow = false, ReleaseDate = new DateTime(2017, 11, 3), AverageRate = 4 };
        var movie12 = new Movie { Title = "Wonder Woman", CoverImage = imageBytes, Description = "An Amazon princess becomes a hero.", IsShow = false, ReleaseDate = new DateTime(2017, 6, 2), AverageRate = 4 };
        var movie13 = new Movie { Title = "Logan", CoverImage = imageBytes, Description = "An aging Wolverine cares for ailing Professor X.", IsShow = false, ReleaseDate = new DateTime(2017, 3, 3), AverageRate = 5 };
        var movie14 = new Movie { Title = "Les Misérables", CoverImage = imageBytes, Description = "A man is hunted for decades in post-revolution France.", IsShow = false, ReleaseDate = new DateTime(2012, 12, 25), AverageRate = 4 };
        var movie15 = new Movie { Title = "Pirates of the Caribbean", CoverImage = imageBytes, Description = "A pirate seeks the cursed treasure.", IsShow = false, ReleaseDate = new DateTime(2003, 7, 9), AverageRate = 4 };
        var movie16 = new Movie { Title = "The Dark Knight", CoverImage = imageBytes, Description = "Batman faces the Joker.", IsShow = false, ReleaseDate = new DateTime(2008, 7, 18), AverageRate = 5 };
        var movie17 = new Movie { Title = "Barbie", CoverImage = imageBytes, Description = "Barbie ventures into the real world.", IsShow = false, ReleaseDate = new DateTime(2023, 7, 21), AverageRate = 4 };
        var movie18 = new Movie { Title = "The Martian", CoverImage = imageBytes, Description = "An astronaut is stranded on Mars.", IsShow = false, ReleaseDate = new DateTime(2015, 10, 2), AverageRate = 4 };
        var movie19 = new Movie { Title = "The Hunger Games", CoverImage = imageBytes, Description = "A girl fights for survival in a deadly game.", IsShow = false, ReleaseDate = new DateTime(2012, 3, 23), AverageRate = 4 };
        var movie20 = new Movie { Title = "Inception", CoverImage = inception, Description = "A thief steals secrets through dreams.", IsShow = false, ReleaseDate = new DateTime(2010, 7, 16), AverageRate = 5 };

        var show1 = new Movie { Title = "Breaking Bad", CoverImage = showImageBytes, Description = "A chemistry teacher becomes a meth producer.", IsShow = true, ReleaseDate = new DateTime(2008, 1, 20), AverageRate = 5 };
        var show2 = new Movie { Title = "Game of Thrones", CoverImage = showImageBytes, Description = "Noble families fight for control of the Iron Throne.", IsShow = true, ReleaseDate = new DateTime(2011, 4, 17), AverageRate = 5 };
        var show3 = new Movie { Title = "Stranger Things", CoverImage = showImageBytes, Description = "Kids uncover supernatural mysteries in Hawkins.", IsShow = true, ReleaseDate = new DateTime(2016, 7, 15), AverageRate = 4 };
        var show4 = new Movie { Title = "Friends", CoverImage = showImageBytes, Description = "Six friends navigate life in New York City.", IsShow = true, ReleaseDate = new DateTime(1994, 9, 22), AverageRate = 5 };
        var show5 = new Movie { Title = "The Office", CoverImage = showImageBytes, Description = "A mockumentary about office employees.", IsShow = true, ReleaseDate = new DateTime(2005, 3, 24), AverageRate = 4 };
        var show6 = new Movie { Title = "Scandal", CoverImage = showImageBytes, Description = "A crisis management expert saves political careers.", IsShow = true, ReleaseDate = new DateTime(2012, 4, 5), AverageRate = 3 };
        var show7 = new Movie { Title = "The Mandalorian", CoverImage = showImageBytes, Description = "A lone bounty hunter protects a mysterious child.", IsShow = true, ReleaseDate = new DateTime(2019, 11, 12), AverageRate = 4 };
        var show8 = new Movie { Title = "The X-Files", CoverImage = showImageBytes, Description = "FBI agents investigate paranormal cases.", IsShow = true, ReleaseDate = new DateTime(1993, 9, 10), AverageRate = 4 };
        var show9 = new Movie { Title = "The Sopranos", CoverImage = showImageBytes, Description = "A mob boss balances family and crime.", IsShow = true, ReleaseDate = new DateTime(1999, 1, 10), AverageRate = 5 };
        var show10 = new Movie { Title = "House of Cards", CoverImage = showImageBytes, Description = "A politician rises to power in Washington.", IsShow = true, ReleaseDate = new DateTime(2013, 2, 1), AverageRate = 4 };
        var show11 = new Movie { Title = "Grey's Anatomy", CoverImage = showImageBytes, Description = "Doctors face challenges in a Seattle hospital.", IsShow = true, ReleaseDate = new DateTime(2005, 3, 27), AverageRate = 3 };
        var show12 = new Movie { Title = "How I Met Your Mother", CoverImage = showImageBytes, Description = "A man recounts how he met the mother of his children.", IsShow = true, ReleaseDate = new DateTime(2005, 9, 19), AverageRate = 4 };
        var show13 = new Movie { Title = "Westworld", CoverImage = showImageBytes, Description = "Humans interact with androids in a theme park.", IsShow = true, ReleaseDate = new DateTime(2016, 10, 2), AverageRate = 4 };
        var show14 = new Movie { Title = "The Crown", CoverImage = showImageBytes, Description = "The reign of Queen Elizabeth II.", IsShow = true, ReleaseDate = new DateTime(2016, 11, 4), AverageRate = 5 };
        var show15 = new Movie { Title = "Ozark", CoverImage = showImageBytes, Description = "A family launders money for a cartel.", IsShow = true, ReleaseDate = new DateTime(2017, 7, 21), AverageRate = 4 };
        var show16 = new Movie { Title = "Sherlock", CoverImage = showImageBytes, Description = "A modern take on Sherlock Holmes.", IsShow = true, ReleaseDate = new DateTime(2010, 7, 25), AverageRate = 5 };
        var show17 = new Movie { Title = "Mindhunter", CoverImage = showImageBytes, Description = "FBI agents profile serial killers.", IsShow = true, ReleaseDate = new DateTime(2017, 10, 13), AverageRate = 4 };
        var show18 = new Movie { Title = "The Boys", CoverImage = showImageBytes, Description = "A group exposes corrupt superheroes.", IsShow = true, ReleaseDate = new DateTime(2019, 7, 26), AverageRate = 5 };
        var show19 = new Movie { Title = "Euphoria", CoverImage = showImageBytes, Description = "Teenagers navigate drugs, love, and trauma.", IsShow = true, ReleaseDate = new DateTime(2019, 6, 16), AverageRate = 4 };
        var show20 = new Movie { Title = "Stranger", CoverImage = showImageBytes, Description = "A prosecutor investigates corruption in Korea.", IsShow = true, ReleaseDate = new DateTime(2017, 6, 10), AverageRate = 4 };


        dataContext.Movies.AddRange(
            movie1, movie2, movie3, movie4, movie5,
            movie6, movie7, movie8, movie9, movie10,
            movie11, movie12, movie13, movie14, movie15,
            movie16, movie17, movie18, movie19, movie20,

            show1, show2, show3, show4, show5,
            show6, show7, show8, show9, show10,
            show11, show12, show13, show14, show15,
            show16, show17, show18, show19, show20
        );

        dataContext.SaveChanges();


        dataContext.MovieRatings.AddRange(
            // Titanic
            new MovieRating { MovieId = movie1.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie1.MovieId, Rate = 4 },

            // The Shawshank Redemption
            new MovieRating { MovieId = movie2.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie2.MovieId, Rate = 3 },

            // Fight Club
            new MovieRating { MovieId = movie3.MovieId, Rate = 4 },
            new MovieRating { MovieId = movie3.MovieId, Rate = 2 },

            // Black Swan
            new MovieRating { MovieId = movie4.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie4.MovieId, Rate = 3 },

            // The Matrix
            new MovieRating { MovieId = movie5.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie5.MovieId, Rate = 1 },

            // Forrest Gump
            new MovieRating { MovieId = movie6.MovieId, Rate = 4 },
            new MovieRating { MovieId = movie6.MovieId, Rate = 2 },

            // La La Land
            new MovieRating { MovieId = movie7.MovieId, Rate = 3 },
            new MovieRating { MovieId = movie7.MovieId, Rate = 4 },

            // Iron Man
            new MovieRating { MovieId = movie8.MovieId, Rate = 4 },
            new MovieRating { MovieId = movie8.MovieId, Rate = 3 },

            // Avengers: Endgame
            new MovieRating { MovieId = movie9.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie9.MovieId, Rate = 2 },

            // Training Day
            new MovieRating { MovieId = movie10.MovieId, Rate = 3 },
            new MovieRating { MovieId = movie10.MovieId, Rate = 1 },

            // Thor: Ragnarok
            new MovieRating { MovieId = movie11.MovieId, Rate = 4 },
            new MovieRating { MovieId = movie11.MovieId, Rate = 3 },

            // Wonder Woman
            new MovieRating { MovieId = movie12.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie12.MovieId, Rate = 2 },

            // Logan
            new MovieRating { MovieId = movie13.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie13.MovieId, Rate = 4 },

            // Les Misérables
            new MovieRating { MovieId = movie14.MovieId, Rate = 3 },
            new MovieRating { MovieId = movie14.MovieId, Rate = 2 },

            // Pirates of the Caribbean
            new MovieRating { MovieId = movie15.MovieId, Rate = 4 },
            new MovieRating { MovieId = movie15.MovieId, Rate = 1 },

            // The Dark Knight
            new MovieRating { MovieId = movie16.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie16.MovieId, Rate = 5 },

            // Barbie
            new MovieRating { MovieId = movie17.MovieId, Rate = 3 },
            new MovieRating { MovieId = movie17.MovieId, Rate = 2 },

            // The Martian
            new MovieRating { MovieId = movie18.MovieId, Rate = 4 },
            new MovieRating { MovieId = movie18.MovieId, Rate = 3 },

            // The Hunger Games
            new MovieRating { MovieId = movie19.MovieId, Rate = 2 },
            new MovieRating { MovieId = movie19.MovieId, Rate = 3 },

            // Inception
            new MovieRating { MovieId = movie20.MovieId, Rate = 5 },
            new MovieRating { MovieId = movie20.MovieId, Rate = 1 },

            new MovieRating { MovieId = show1.MovieId, Rate = 5 },
            new MovieRating { MovieId = show1.MovieId, Rate = 4 },
            new MovieRating { MovieId = show2.MovieId, Rate = 5 },
            new MovieRating { MovieId = show2.MovieId, Rate = 3 },       
            new MovieRating { MovieId = show3.MovieId, Rate = 4 },
            new MovieRating { MovieId = show3.MovieId, Rate = 2 },          
            new MovieRating { MovieId = show4.MovieId, Rate = 5 },
            new MovieRating { MovieId = show4.MovieId, Rate = 4 },           
            new MovieRating { MovieId = show5.MovieId, Rate = 4 },
            new MovieRating { MovieId = show5.MovieId, Rate = 3 },
            new MovieRating { MovieId = show6.MovieId, Rate = 3 },
            new MovieRating { MovieId = show6.MovieId, Rate = 2 },           
            new MovieRating { MovieId = show7.MovieId, Rate = 4 },
            new MovieRating { MovieId = show7.MovieId, Rate = 5 },        
            new MovieRating { MovieId = show8.MovieId, Rate = 4 },
            new MovieRating { MovieId = show8.MovieId, Rate = 2 },          
            new MovieRating { MovieId = show9.MovieId, Rate = 5 },
            new MovieRating { MovieId = show9.MovieId, Rate = 3 },
            new MovieRating { MovieId = show10.MovieId, Rate = 4 },
            new MovieRating { MovieId = show10.MovieId, Rate = 2 },            
            new MovieRating { MovieId = show11.MovieId, Rate = 3 },
            new MovieRating { MovieId = show11.MovieId, Rate = 4 },          
            new MovieRating { MovieId = show12.MovieId, Rate = 4 },
            new MovieRating { MovieId = show12.MovieId, Rate = 5 },         
            new MovieRating { MovieId = show13.MovieId, Rate = 4 },
            new MovieRating { MovieId = show13.MovieId, Rate = 3 },          
            new MovieRating { MovieId = show14.MovieId, Rate = 5 },
            new MovieRating { MovieId = show14.MovieId, Rate = 4 },      
            new MovieRating { MovieId = show15.MovieId, Rate = 4 },
            new MovieRating { MovieId = show15.MovieId, Rate = 3 },       
            new MovieRating { MovieId = show16.MovieId, Rate = 5 },
            new MovieRating { MovieId = show16.MovieId, Rate = 4 },       
            new MovieRating { MovieId = show17.MovieId, Rate = 4 },
            new MovieRating { MovieId = show17.MovieId, Rate = 3 },        
            new MovieRating { MovieId = show18.MovieId, Rate = 5 },
            new MovieRating { MovieId = show18.MovieId, Rate = 4 },   
            new MovieRating { MovieId = show19.MovieId, Rate = 4 },
            new MovieRating { MovieId = show19.MovieId, Rate = 3 },
            new MovieRating { MovieId = show20.MovieId, Rate = 4 },
            new MovieRating { MovieId = show20.MovieId, Rate = 2 }
         );

        dataContext.SaveChanges();

        dataContext.MovieActors.AddRange(
            // Titanic
            new MovieActor { MovieId = movie1.MovieId, ActorId = actor1.ActorId },  // Leonardo DiCaprio
            new MovieActor { MovieId = movie1.MovieId, ActorId = actor2.ActorId },  // Kate Winslet

            // The Shawshank Redemption
            new MovieActor { MovieId = movie2.MovieId, ActorId = actor3.ActorId },  // Morgan Freeman
            new MovieActor { MovieId = movie2.MovieId, ActorId = actor7.ActorId },  // Tom Hanks (fictional pairing here)

            // Fight Club
            new MovieActor { MovieId = movie3.MovieId, ActorId = actor4.ActorId },  // Brad Pitt
            new MovieActor { MovieId = movie3.MovieId, ActorId = actor17.ActorId }, // Christian Bale (filler)

            // Black Swan
            new MovieActor { MovieId = movie4.MovieId, ActorId = actor5.ActorId },  // Natalie Portman
            new MovieActor { MovieId = movie4.MovieId, ActorId = actor15.ActorId }, // Anne Hathaway

            // The Matrix
            new MovieActor { MovieId = movie5.MovieId, ActorId = actor6.ActorId },  // Keanu Reeves
            new MovieActor { MovieId = movie5.MovieId, ActorId = actor13.ActorId }, // Gal Gadot (filler)

            // Forrest Gump
            new MovieActor { MovieId = movie6.MovieId, ActorId = actor7.ActorId },  // Tom Hanks
            new MovieActor { MovieId = movie6.MovieId, ActorId = actor15.ActorId }, // Anne Hathaway (filler)

            // La La Land
            new MovieActor { MovieId = movie7.MovieId, ActorId = actor8.ActorId },  // Emma Stone
            new MovieActor { MovieId = movie7.MovieId, ActorId = actor4.ActorId },  // Brad Pitt (filler)

            // Iron Man
            new MovieActor { MovieId = movie8.MovieId, ActorId = actor9.ActorId },  // Robert Downey Jr.
            new MovieActor { MovieId = movie8.MovieId, ActorId = actor10.ActorId }, // Scarlett Johansson

            // Avengers: Endgame
            new MovieActor { MovieId = movie9.MovieId, ActorId = actor9.ActorId },  // Robert Downey Jr.
            new MovieActor { MovieId = movie9.MovieId, ActorId = actor12.ActorId }, // Chris Hemsworth
            new MovieActor { MovieId = movie9.MovieId, ActorId = actor10.ActorId }, // Scarlett Johansson

            // Training Day
            new MovieActor { MovieId = movie10.MovieId, ActorId = actor11.ActorId }, // Denzel Washington
            new MovieActor { MovieId = movie10.MovieId, ActorId = actor19.ActorId }, // Matt Damon (filler)

            // Thor: Ragnarok
            new MovieActor { MovieId = movie11.MovieId, ActorId = actor12.ActorId }, // Chris Hemsworth
            new MovieActor { MovieId = movie11.MovieId, ActorId = actor13.ActorId }, // Gal Gadot (filler)

            // Wonder Woman
            new MovieActor { MovieId = movie12.MovieId, ActorId = actor13.ActorId }, // Gal Gadot
            new MovieActor { MovieId = movie12.MovieId, ActorId = actor12.ActorId }, // Chris Hemsworth (filler)

            // Logan
            new MovieActor { MovieId = movie13.MovieId, ActorId = actor14.ActorId }, // Hugh Jackman
            new MovieActor { MovieId = movie13.MovieId, ActorId = actor15.ActorId }, // Anne Hathaway (filler)

            // Les Misérables
            new MovieActor { MovieId = movie14.MovieId, ActorId = actor15.ActorId }, // Anne Hathaway
            new MovieActor { MovieId = movie14.MovieId, ActorId = actor14.ActorId }, // Hugh Jackman

            // Pirates of the Caribbean
            new MovieActor { MovieId = movie15.MovieId, ActorId = actor16.ActorId }, // Johnny Depp
            new MovieActor { MovieId = movie15.MovieId, ActorId = actor2.ActorId },  // Kate Winslet (filler)

            // The Dark Knight
            new MovieActor { MovieId = movie16.MovieId, ActorId = actor17.ActorId }, // Christian Bale
            new MovieActor { MovieId = movie16.MovieId, ActorId = actor9.ActorId },  // Robert Downey Jr. (filler)

            // Barbie
            new MovieActor { MovieId = movie17.MovieId, ActorId = actor18.ActorId }, // Margot Robbie
            new MovieActor { MovieId = movie17.MovieId, ActorId = actor4.ActorId },  // Brad Pitt (filler)

            // The Martian
            new MovieActor { MovieId = movie18.MovieId, ActorId = actor19.ActorId }, // Matt Damon
            new MovieActor { MovieId = movie18.MovieId, ActorId = actor5.ActorId },  // Natalie Portman (filler)

            // The Hunger Games
            new MovieActor { MovieId = movie19.MovieId, ActorId = actor20.ActorId }, // Jennifer Lawrence
            new MovieActor { MovieId = movie19.MovieId, ActorId = actor17.ActorId }, // Christian Bale (filler)

            // Inception
            new MovieActor { MovieId = movie20.MovieId, ActorId = actor1.ActorId },  // Leonardo DiCaprio
            new MovieActor { MovieId = movie20.MovieId, ActorId = actor15.ActorId }  // Anne Hathaway (filler)
        );

        dataContext.SaveChanges();

        dataContext.MovieActors.AddRange(
            new MovieActor { MovieId = show1.MovieId, ActorId = showActor1.ActorId },
            new MovieActor { MovieId = show1.MovieId, ActorId = showActor2.ActorId },

            new MovieActor { MovieId = show2.MovieId, ActorId = showActor3.ActorId },
            new MovieActor { MovieId = show2.MovieId, ActorId = showActor4.ActorId },

            new MovieActor { MovieId = show3.MovieId, ActorId = showActor7.ActorId },
            new MovieActor { MovieId = show3.MovieId, ActorId = showActor6.ActorId },

            new MovieActor { MovieId = show4.MovieId, ActorId = showActor8.ActorId },
            new MovieActor { MovieId = show4.MovieId, ActorId = showActor9.ActorId },

            new MovieActor { MovieId = show5.MovieId, ActorId = showActor11.ActorId },
            new MovieActor { MovieId = show5.MovieId, ActorId = showActor12.ActorId },

            new MovieActor { MovieId = show6.MovieId, ActorId = showActor13.ActorId },
            new MovieActor { MovieId = show6.MovieId, ActorId = showActor14.ActorId },

            new MovieActor { MovieId = show7.MovieId, ActorId = showActor16.ActorId },
            new MovieActor { MovieId = show7.MovieId, ActorId = showActor7.ActorId },

            new MovieActor { MovieId = show8.MovieId, ActorId = showActor17.ActorId },
            new MovieActor { MovieId = show8.MovieId, ActorId = showActor18.ActorId },

            new MovieActor { MovieId = show9.MovieId, ActorId = showActor20.ActorId },
            new MovieActor { MovieId = show9.MovieId, ActorId = showActor19.ActorId },

            new MovieActor { MovieId = show10.MovieId, ActorId = showActor13.ActorId },
            new MovieActor { MovieId = show10.MovieId, ActorId = showActor14.ActorId },

            new MovieActor { MovieId = show11.MovieId, ActorId = showActor11.ActorId },
            new MovieActor { MovieId = show11.MovieId, ActorId = showActor12.ActorId },

            new MovieActor { MovieId = show12.MovieId, ActorId = showActor8.ActorId },
            new MovieActor { MovieId = show12.MovieId, ActorId = showActor9.ActorId },

            new MovieActor { MovieId = show13.MovieId, ActorId = showActor3.ActorId },
            new MovieActor { MovieId = show13.MovieId, ActorId = showActor4.ActorId },

            new MovieActor { MovieId = show14.MovieId, ActorId = showActor5.ActorId },
            new MovieActor { MovieId = show14.MovieId, ActorId = showActor6.ActorId },

            new MovieActor { MovieId = show15.MovieId, ActorId = showActor16.ActorId },
            new MovieActor { MovieId = show15.MovieId, ActorId = showActor13.ActorId },

            new MovieActor { MovieId = show16.MovieId, ActorId = showActor1.ActorId },
            new MovieActor { MovieId = show16.MovieId, ActorId = showActor2.ActorId },

            new MovieActor { MovieId = show17.MovieId, ActorId = showActor7.ActorId },
            new MovieActor { MovieId = show17.MovieId, ActorId = showActor6.ActorId },

            new MovieActor { MovieId = show18.MovieId, ActorId = showActor16.ActorId },
            new MovieActor { MovieId = show18.MovieId, ActorId = showActor20.ActorId },

            new MovieActor { MovieId = show19.MovieId, ActorId = showActor7.ActorId },
            new MovieActor { MovieId = show19.MovieId, ActorId = showActor6.ActorId },

            new MovieActor { MovieId = show20.MovieId, ActorId = showActor13.ActorId },
            new MovieActor { MovieId = show20.MovieId, ActorId = showActor14.ActorId }
        );
        
        dataContext.SaveChanges();
    }
}


static string GenerateSalt()
{
    var provider = new RNGCryptoServiceProvider();
    var byteArray = new byte[16];
    provider.GetBytes(byteArray);

    return Convert.ToBase64String(byteArray);
}

static string GenerateHash(string salt, string password)
{
    byte[] src = Convert.FromBase64String(salt);
    byte[] bytes = Encoding.Unicode.GetBytes(password);
    byte[] dist = new byte[src.Length + bytes.Length];

    System.Buffer.BlockCopy(src, 0, dist, 0, src.Length);
    System.Buffer.BlockCopy(bytes, 0, dist, src.Length, bytes.Length);

    HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
    byte[] inArray = algorithm.ComputeHash(dist);
    return Convert.ToBase64String(inArray);
}
app.Run();
