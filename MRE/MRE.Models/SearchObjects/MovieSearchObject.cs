using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRE.Models.SearchObjects
{
    public class MovieSearchObject : BaseSearchObject
    {
        public string? FTS { get; set; }
        public bool? isShow { get; set; } = false;
    }
}
