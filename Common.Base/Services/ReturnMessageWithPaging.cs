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
    public class ReturnMessageWithPaging<T>
    {
        [JsonConstructor]
        public ReturnMessageWithPaging()
        {
            ServiceResult = ServiceResult.Success();
        }

        [JsonProperty("serviceResult")]
        [DataMember(IsRequired = true)]
        public ServiceResult ServiceResult { get; private set; }

        [JsonProperty("data")]
        [DataMember(IsRequired = true)]
        public Data<T> Data { get; private set; }

        public ReturnMessageWithPaging(Data<T> data, ServiceResult serviceResult)
        {
            Data = data;
            ServiceResult = serviceResult;
        }

        public ReturnMessageWithPaging(ServiceResult serviceResult)
        {
            ServiceResult = serviceResult;
            Data = null!;
        }

        public static ReturnMessageWithPaging<T> Success(Data<T> data)
        {
            var serviceResult = ServiceResult.Success();
            return new ReturnMessageWithPaging<T>(data, serviceResult);
        }

        public static ReturnMessageWithPaging<T> Failure(BusinessRuleException bex)
        {
            var serviceResult = ServiceResult.Fail(bex.Message);
            return new ReturnMessageWithPaging<T>(serviceResult);
        }

        public static ReturnMessageWithPaging<T> Failure(string message)
        {
            var serviceResult = ServiceResult.Fail(message);
            return new ReturnMessageWithPaging<T>(serviceResult);
        }

        public static ReturnMessageWithPaging<T> Error(ValidationException vex)
        {
            var serviceResult = ServiceResult.Error(vex.Message);
            return new ReturnMessageWithPaging<T>(serviceResult);
        }

        public static ReturnMessageWithPaging<T> GeneralError(System.Exception ex)
        {
            var serviceResult = ServiceResult.GlobalError(ex.Message);
            return new ReturnMessageWithPaging<T>(serviceResult);
        }
    }
}
