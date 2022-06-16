namespace Core.Entities
{
    public class Account
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? ActivateCode { get; set; }
        public bool IsActive { get; set; }
    }
}