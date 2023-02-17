using System.Text.Json;
using System.Text;
using SoftTeK.BusinessAdvisors.Web.Helpers;

namespace SoftTeK.BusinessAdvisors.Web.Repository
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        private JsonSerializerOptions DefaultJSON => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            try
            {
                var responseHTTP = await _httpClient.GetAsync(url);

                if (responseHTTP.IsSuccessStatusCode)
                {
                    var response = await DeserealizarRespuesta<T>(responseHTTP, DefaultJSON);

                    return new HttpResponseWrapper<T>(response, false, responseHTTP);
                }
                else if (responseHTTP.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                {
                    var stream = await responseHTTP.Content.ReadAsStreamAsync();
                    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web); //note: cache and reuse this
                    var problemDetails = JsonSerializer.Deserialize<ProblemDetailsWithErrors>(stream, jsonOptions);

                    Console.WriteLine(JsonSerializer.Serialize(problemDetails));
                }

                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T send)
        {
            var sendJson = JsonSerializer.Serialize(send);
            var sendContent = new StringContent(sendJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, sendContent);

            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T send)
        {
            var sendJson = JsonSerializer.Serialize(send);
            var sendContent = new StringContent(sendJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, sendContent);

            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T send)
        {
            var sendJson = JsonSerializer.Serialize(send);
            var sendContent = new StringContent(sendJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, sendContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserealizarRespuesta<TResponse>(responseHttp, DefaultJSON);

                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var reponseHttp = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !reponseHttp.IsSuccessStatusCode, reponseHttp);
        }

        private async Task<T> DeserealizarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var resposeString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(resposeString, jsonSerializerOptions);
        }
    }
}
