using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRE.Models
{
    public class Actors
    {
        public int ActorId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        //public virtual ICollection<MovieActors> MovieActors { get; set; } = new List<MovieActors>();
    }
}
