using Common.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Utils
{
    public class OrderDetailGeneric<T>
    {
        public Expression<Func<T, object>> OrderColumn { get; }
        public OrderType OrderType { get; }

        public OrderDetailGeneric(Expression<Func<T, object>> orderColumn, OrderType orderType)
        {
            OrderColumn = orderColumn;
            OrderType = orderType;
        }
    }
}
