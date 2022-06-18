namespace Application.Models.Tokens
{
    public class CreateTokenResponse
    {
        public string? AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}