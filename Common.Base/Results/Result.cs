using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Results
{
    [DataContract]
    public enum Result
    {
        [EnumMember]
        Success,

        [EnumMember]
        Warning,

        [EnumMember]
        Failure,

        [EnumMember]
        Error
    }
}
