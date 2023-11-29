namespace AuthenticatorAPI.Models.Dto
{
    public class UpdateRolesDto
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
