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

        var actor1 = new Actor
        {
            FirstName = "Actor1",
            LastName = "Actor1",

        };
        var actor2 = new Actor
        {
            FirstName = "Actor2",
            LastName = "Actor2",

        };

        dataContext.Actors.AddRange(
       
        actor1,
        actor2
           );

        dataContext.SaveChanges();


        string filePath = @"C:\Users\Korisnik\Desktop\diplomski\logo\fit.jpg";
        byte[] imageBytes= File.ReadAllBytes( filePath );

        var movie1 = new Movie
        {
            Title = "title of movie1",
            CoverImage = imageBytes,
            Description = "short description",
            IsShow = false,
            ReleaseDate = DateTime.Now,
            AverageRate = 5

        };

        var movie2 = new Movie
        {
            Title = "title of movie2",
            CoverImage = imageBytes,
            Description = "short description",
            IsShow = false,
            ReleaseDate = DateTime.Now,
            AverageRate = 4

        };

        dataContext.Movies.AddRange(
         movie1,
         movie2
           );

        dataContext.SaveChanges();


        dataContext.MovieRatings.AddRange(
      new MovieRating
      {
          MovieId = movie1.MovieId,
          Rate = 5

      },
      new MovieRating
      {
          MovieId = movie2.MovieId,
          Rate = 4
      }
        );

        dataContext.SaveChanges();


        var movieactor1 = new MovieActor
        {
            MovieId = movie1.MovieId,
            ActorId = actor1.ActorId

        };
        var movieactor2 = new MovieActor
        {
            MovieId = movie1.MovieId,
            ActorId = actor2.ActorId

        };
        var movieactor3 = new MovieActor
        {
            MovieId = movie2.MovieId,
            ActorId = actor1.ActorId

        };
        var movieactor4 = new MovieActor
        {
            MovieId = movie2.MovieId,
            ActorId = actor2.ActorId

        };

        dataContext.MovieActors.AddRange(
            movieactor1,
            movieactor2,
            movieactor3,
            movieactor4

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
