using Common.Base.Exceptions;
using Common.Base.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Services
{
    [DataContract]
    public class ReturnMessageWithList<T>
    {
        [JsonConstructor]
        public ReturnMessageWithList()
        {
            ServiceResult = ServiceResult.Success();
        }

        [JsonProperty("serviceResult")]
        [DataMember(IsRequired = true)]
        public ServiceResult ServiceResult { get; private set; }

        [JsonProperty("data")]
        [DataMember(IsRequired = true)]
        public IEnumerable<T>? Data { get; private set; }

        public ReturnMessageWithList(IEnumerable<T> data, ServiceResult serviceResult)
        {
            Data = data;
            ServiceResult = serviceResult;
        }

        public ReturnMessageWithList(ServiceResult serviceResult)
        {
            Data = Enumerable.Empty<T>();
            ServiceResult = serviceResult;
        }

        public static ReturnMessageWithList<T> Success(IEnumerable<T> data)
        {
            var result = ServiceResult.Success();
            return new ReturnMessageWithList<T>(data, result);
        }

        public static ReturnMessageWithList<T> Failure(BusinessRuleException bex)
        {
            var result = ServiceResult.Fail(bex.Message);
            return new ReturnMessageWithList<T>(result);
        }

        public static ReturnMessageWithList<T> Failure(string message)
        {
            var result = ServiceResult.Fail(message);
            return new ReturnMessageWithList<T>(result);
        }

        public static ReturnMessageWithList<T> Error(ValidationException vex)
        {
            var result = ServiceResult.Error(vex.Message);
            return new ReturnMessageWithList<T>(result);
        }

        public static ReturnMessageWithList<T> GlobalError(System.Exception ex)
        {
            var result = ServiceResult.GlobalError(ex.Message);
            return new ReturnMessageWithList<T>(result);
        }
    }
}
