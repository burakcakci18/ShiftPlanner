using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Exceptions
{
    public class BusinessRuleException : System.Exception
    {
        public int Code { get; }

        public BusinessRuleException(string message, int code = 1000) : base(message)
        {
            Code = code;
        }

        public BusinessRuleException(string message, System.Exception innerException, int code = 1000)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}
