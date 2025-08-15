using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MRE.Models.Requests
{
    public class MovieRatingsInsertRequest
    {
        [Required]
        public int? MovieId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        [JsonIgnore]
        public DateTime? RateDate { get; set; } = DateTime.Now;
    }
}
