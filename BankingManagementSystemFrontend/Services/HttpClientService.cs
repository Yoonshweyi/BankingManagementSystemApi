using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace BankingManagementSystemFrontend.Services
{
    public class httpClientService
    {
        private readonly HttpClient _httpClient;

        public httpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<T> ExecuteAsync<T>(string endpoint, EnumHttpMethod httpMethod, object? requestModel = null)
        {
            HttpResponseMessage? response = null;
            HttpContent content = null;
            if (requestModel is not null)
            {
                var json = JsonConvert.SerializeObject(requestModel);
                content = new StringContent(json, Encoding.UTF8, Application.Json);
            }
            switch (httpMethod)
            {
                case EnumHttpMethod.Get:
                    response = await _httpClient.GetAsync(endpoint);
                    break;
                case EnumHttpMethod.Post:
                    response = await _httpClient.PostAsync(endpoint, content);
                    break;
                case EnumHttpMethod.Put:
                    response = await _httpClient.PutAsync(endpoint, content);
                    break;
                case EnumHttpMethod.Delete:
                    response = await _httpClient.DeleteAsync(endpoint);
                    break;
                case EnumHttpMethod.None:
                default:
                    throw new Exception("Invalid EnumHttpMethod.");
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseJson);
            var model = JsonConvert.DeserializeObject<T>(responseJson);
            return model!;
           
        }

    }
    public enum EnumHttpMethod
    {
        None,
        Get,
        Post,
        Put,
        Delete
    }
}
