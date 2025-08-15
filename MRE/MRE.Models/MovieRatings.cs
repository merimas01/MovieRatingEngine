using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRE.Models
{
    public class MovieRatings
    {
        public int MovieRatingId { get; set; }

        public int? MovieId { get; set; }

        public int Rate { get; set; }

        public DateTime? RateDate { get; set; }

        //public virtual Movies? Movie { get; set; }
    }
}
