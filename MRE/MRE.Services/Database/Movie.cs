using System;
using System.Collections.Generic;

namespace MRE.Services.Database;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public byte[] CoverImage { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public bool? IsShow { get; set; }

    public decimal? AverageRate { get; set; }

    public virtual ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

    public virtual ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();
}
