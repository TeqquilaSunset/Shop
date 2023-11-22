using System.ComponentModel.DataAnnotations;

namespace AuthenticatorAPI.Models.Request
{
    /// <summary>
    /// Запрос для регистрации
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Phone(ErrorMessage = "Введите корректный номер телефона.")]
        public string? PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Электронная почта обязательна для заполнения.")]
        [EmailAddress(ErrorMessage = "Введите корректный адрес электронной почты.")]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Введите корректную дату рождения.")]
        public DateTime? DateOfBirth { get; set; }
    }
}
