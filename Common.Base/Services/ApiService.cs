using Common.Base.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ReturnMessage<RESPONSETYPE>> DefaultApiPostServiceAsync<REQUESTTYPE, RESPONSETYPE>(
            string apiUrl,
            REQUESTTYPE request,
            Dictionary<string, string>? requestHeader = null)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                        Encoding.UTF8,
                        "application/json")
                };

                if (requestHeader != null)
                {
                    foreach (var header in requestHeader)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                var response = await _httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ReturnMessage<RESPONSETYPE>(new ServiceResult
                    {
                        ErrorCode = "GENERAL_ERROR",
                        ErrorMessage = responseContent
                    });
                }

                var data = JsonConvert.DeserializeObject<RESPONSETYPE>(responseContent)!;
                return new ReturnMessage<RESPONSETYPE>(data, ServiceResult.Success());
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex, "Calling {apiUrl} failed.", apiUrl);
                return new ReturnMessage<RESPONSETYPE>(ServiceResult.GlobalError(ex.Message));
            }
        }

        public async Task<ReturnMessage<T>> DefaultApiGetServiceAsync<T>(
            string apiUrl,
            Dictionary<string, string>? requestHeader = null)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (requestHeader != null)
                {
                    foreach (var header in requestHeader)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                var response = await _httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ReturnMessage<T>(new ServiceResult
                    {
                        ErrorCode = "GENERAL_ERROR",
                        ErrorMessage = responseContent
                    });
                }

                var data = JsonConvert.DeserializeObject<T>(responseContent)!;
                return new ReturnMessage<T>(data, ServiceResult.Success());
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex, "Calling {apiUrl} failed.", apiUrl);
                return new ReturnMessage<T>(ServiceResult.GlobalError(ex.Message));
            }
        }
    }
}
