using Data.DTO.Payment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Services.Payment
{
    public class IpgRestServiceClient
    {
        private readonly string _serviceAddress;
        private const string JsonContentType = "application/json";
        private static volatile HttpClient _httpClient;
        private static readonly object SyncRoot = new object();
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        private HttpClient GetClient()
        {
            if (_httpClient == null)
            {
                lock (SyncRoot)
                {
                    if (_httpClient == null)
                    {
                        _httpClient = new HttpClient { BaseAddress = new Uri(_serviceAddress) };
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonContentType));
                    }
                }
            }

            return _httpClient;
        }
        public IpgRestServiceClient(string baseUrl)
        {
            _serviceAddress = baseUrl;
        }
        public async Task<PaymentResponseDTO> PaymentRequestAsync(PaymentRequestDTO request)
        {
            var client = GetClient();
            var httpResponse = await client.PostAsync("Payment/rest/v1/Request", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType));
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var t = httpResponse.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<PaymentResponseDTO>(t, jsonOptions);
            }
            else throw new Exception($"Service Call Http Response Code:{(int)httpResponse.StatusCode}");
        }

        public async Task<VerifyResponseDTO> PaymentVerificationAsync(VerifyRequestDTO request)
        {
            var client = GetClient();
            var httpResponse = await client.PostAsync("Payment/rest/v1/Verification", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType));
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var t = httpResponse.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<VerifyResponseDTO>(t, jsonOptions);
            }
            else throw new Exception($"Service Call Http Response Code:{(int)httpResponse.StatusCode}");
        }

    }
}
