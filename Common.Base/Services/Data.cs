using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Services
{
    public class Data<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public Paging Paging { get; set; } = new();
    }
}
