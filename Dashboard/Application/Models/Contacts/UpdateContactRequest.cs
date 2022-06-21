namespace Application.Models.Contacts
{
    public class UpdateContactRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Project { get; set; }
        public string? Avatar { get; set; }
    }
}