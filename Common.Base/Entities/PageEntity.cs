using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Entities
{
    public class PageEntity
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortName { get; set; }
        public string SortType { get; set; } = "DESC";
    }
}
