namespace Application.Models.Tokens
{
    public class TokenDto
    {
        public string? AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}