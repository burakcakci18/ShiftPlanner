using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Utils
{
    public class OrderDetail
    {
        public string Field { get; set; } = string.Empty;
        public string Direction { get; set; } = "asc";

        public OrderDetail() { }

        public OrderDetail(string field, string direction = "asc")
        {
            Field = field;
            Direction = direction.ToLower() == "desc" ? "desc" : "asc";
        }
    }
}
