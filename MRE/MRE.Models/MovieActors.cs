using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRE.Models
{
    public class MovieActors
    {
        public int MovieActorsId { get; set; }

        public int? MovieId { get; set; }

        public int? ActorId { get; set; }

        public virtual Actors? Actor { get; set; }

       // public virtual Movies? Movie { get; set; }
    }
}
