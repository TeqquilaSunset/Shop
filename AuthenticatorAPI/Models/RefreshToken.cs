namespace AuthenticatorAPI.Models
{
    /// <summary>
    /// Класс для работы с refresh токеном
    /// </summary>
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
