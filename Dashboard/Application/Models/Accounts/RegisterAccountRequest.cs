namespace Application.Models.Accounts
{
    public class RegisterAccountRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }

    }
}