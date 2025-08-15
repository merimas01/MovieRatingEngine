using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRE.Models
{
    public class Movies
    {
        public int MovieId { get; set; }

        public string Title { get; set; } = null!;

        public byte[] CoverImage { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public bool? IsShow { get; set; }

        public decimal? AverageRate { get; set; }

        public virtual ICollection<MovieActors> MovieActors { get; set; } = new List<MovieActors>();

        public virtual ICollection<MovieRatings> MovieRatings { get; set; } = new List<MovieRatings>();
    }
}
