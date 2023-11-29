namespace OrderMicroservice.Services
{
    public interface IApiService
    {
        Task<string> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(object obj, string url, string? accessToken);
    }
}
