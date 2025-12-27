using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Results
{
    public class ServiceResult
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; } = "OK";

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; } = "OK";

        [JsonProperty("isSuccess")]
        public bool IsSuccess => ErrorCode == "OK";

        public static ServiceResult Success() => new() { ErrorCode = "OK", ErrorMessage = "OK" };

        public static ServiceResult Fail(string errorMessage = "An error occurred.", string errorCode = "BUSINESS") =>
            new() { ErrorCode = errorCode, ErrorMessage = errorMessage };

        public static ServiceResult Error(string errorMessage = "An error occurred.", string errorCode = "VALIDATION") =>
            new() { ErrorCode = errorCode, ErrorMessage = errorMessage };

        public static ServiceResult GlobalError(string errorMessage = "An error occurred.", string errorCode = "GLOBAL") =>
            new() { ErrorCode = errorCode, ErrorMessage = errorMessage };
    }
}
