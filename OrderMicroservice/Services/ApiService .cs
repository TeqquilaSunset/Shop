
using OrderMicroservice.Model.Dto;
using System.Text;

namespace OrderMicroservice.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Произошла ошибка при выполнении HTTP-GET-запроса: {ex.Message}");
            }
        }


        public async Task<HttpResponseMessage> PostAsync(object obj, string url)
        {

            using (HttpClient httpClient = new HttpClient())
            {

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //content.Headers.Add("accept", "*/*");

                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                return response;
            }
        }
    }
}
