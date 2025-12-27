using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Results
{
    [DataContract]
    public class ResultCode
    {
        public ResultCode(string prefix, int code, string message)
        {
            Code = code;
            Prefix = prefix;
            Message = message;
        }

        [DataMember(Order = 0)]
        public string Prefix { get; private set; } = null!;

        [DataMember(Order = 1)]
        public int Code { get; private set; }

        [DataMember(Order = 2)]
        public string Message { get; private set; }
    }
}
