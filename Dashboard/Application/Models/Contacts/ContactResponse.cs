namespace Application.Models.Contacts
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Project { get; set; }
        public string? Avatar { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}