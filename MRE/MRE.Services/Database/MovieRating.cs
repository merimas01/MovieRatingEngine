using System;
using System.Collections.Generic;

namespace MRE.Services.Database;

public partial class MovieRating
{
    public int MovieRatingId { get; set; }

    public int? MovieId { get; set; }

    public int Rate { get; set; }

    public DateTime? RateDate { get; set; }

    public virtual Movie? Movie { get; set; }
}
