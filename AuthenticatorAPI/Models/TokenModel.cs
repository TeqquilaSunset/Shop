using System.ComponentModel.DataAnnotations;

namespace AuthenticatorAPI.Models
{
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        [Required]
        public required string RefreshToken { get; set; }

    }
}
