namespace DeliveryMicroservice.Services
{
    public interface IApiService
    {
        Task<string> GetAsync(string url);
        Task<HttpResponseMessage> PutAsync(object obj, string url, string? accessToken);
    }
}
