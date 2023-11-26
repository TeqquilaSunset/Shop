using System.Text;

namespace OrderMicroservice.Helper
{
    /// <summary>
    /// Тестовый метод проверки прямого взаимодействия сервисов
    /// </summary>
    public class HttpRequestDelivery
    {
        public async Task<HttpResponseMessage> SendPostRequest(Guid orderId)
        {
            string url = "https://localhost:7009/api/DeliveryMicroservice/Delivery";


            using (HttpClient httpClient = new HttpClient())
            {

                var data = new
                {
                    orderId = orderId
                };

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //content.Headers.Add("accept", "*/*");

                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                return response;
            }
        }
    }
}
