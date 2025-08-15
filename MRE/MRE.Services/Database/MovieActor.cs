using System;
using System.Collections.Generic;

namespace MRE.Services.Database;

public partial class MovieActor
{
    public int MovieActorsId { get; set; }

    public int? MovieId { get; set; }

    public int? ActorId { get; set; }

    public virtual Actor? Actor { get; set; }

    public virtual Movie? Movie { get; set; }
}
