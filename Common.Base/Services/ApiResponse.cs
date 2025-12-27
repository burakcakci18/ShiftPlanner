using Common.Base.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Services
{
    [DataContract]
    public class ApiResponse<T>
    {
        public ApiResponse(T? data, Result result, ResultCode? resultCode, string? state = null)
        {
            Data = data;
            Result = result;
            ResultCode = resultCode;
            State = state;
        }

        [DataMember(IsRequired = true)]
        public Result Result { get; private set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public T? Data { get; private set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ResultCode? ResultCode { get; private set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string? State { get; private set; }

        // Success response
        public static ApiResponse<T> Success(T data)
            => new(data, Result.Success, new ResultCode(string.Empty, 200, "OK"));

        public static ApiResponse<T> Success(T data, string? state)
            => new(data, Result.Success, new ResultCode(string.Empty, 200, "OK"), state);

        // Failure response
        public static ApiResponse<T> Failure(string prefix, int code, string message)
            => new(default, Result.Failure, new ResultCode(prefix, code, message));

        // Error response
        public static ApiResponse<T> Error(string prefix, int code, string message)
            => new(default, Result.Error, new ResultCode(prefix, code, message));

        public static ApiResponse<T> GlobalError(string prefix, int code, string message)
            => new(default, Result.Error, new ResultCode(prefix, code, message));
    }
}
