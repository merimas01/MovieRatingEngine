using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRE.Models
{
    public class PagedResult<T>
    {
        public List<T> Result { get; set; }
        public int? Count { get; set; } //count of filtered results
        public int? CurrentCount { get; set; } 
        public int? TotalCountBeforeFilter { get; set; }
    }
}
