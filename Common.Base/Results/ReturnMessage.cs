using Common.Base.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Results
{
    [DataContract]
    public class ReturnMessage<T>
    {
        [JsonConstructor]
        public ReturnMessage()
        {
            ServiceResult = ServiceResult.Success();
        }

        [JsonProperty("serviceResult")]
        [DataMember(IsRequired = true)]
        public ServiceResult ServiceResult { get; private set; }

        [JsonProperty("data")]
        [DataMember(IsRequired = true)]
        public T? Data { get; private set; }

        public ReturnMessage(T data, ServiceResult serviceResult)
        {
            Data = data;
            ServiceResult = serviceResult;
        }

        public ReturnMessage(ServiceResult serviceResult)
        {
            Data = default;
            ServiceResult = serviceResult;
        }

        public static ReturnMessage<T> Success(T data)
        {
            var result = ServiceResult.Success();
            return new ReturnMessage<T>(data, result);
        }

        public static ReturnMessage<T> Failure(BusinessRuleException bex)
        {
            var result = ServiceResult.Fail(bex.Message);
            return new ReturnMessage<T>(result);
        }

        public static ReturnMessage<T> Failure(string error)
        {
            var result = ServiceResult.Fail(error);
            return new ReturnMessage<T>(result);
        }

        public static ReturnMessage<T> Error(ValidationException vex)
        {
            var result = ServiceResult.Error(vex.Message);
            return new ReturnMessage<T>(result);
        }

        public static ReturnMessage<T> GlobalError(System.Exception ex)
        {
            var result = ServiceResult.GlobalError(ex.Message);
            return new ReturnMessage<T>(result);
        }
    }
}
