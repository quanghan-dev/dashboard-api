using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Department { get; set; }
        public string? Project { get; set; }
        public string? Avatar { get; set; }
        [ForeignKey("Account")]
        public Guid? EmployeeId { get; set; }
        public int Status { get; set; }

        public Account? Account { get; set; }

        public static Contact FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Contact contact = new();
            contact.FirstName = values[0];
            contact.LastName = values[1];
            contact.Title = values[2];
            contact.Department = values[3];
            contact.Project = values[4];
            contact.Avatar = values[5];
            contact.EmployeeId = !string.IsNullOrEmpty(values[6]) ? Guid.Parse(values[6]) : null;

            return contact;
        }

        public override string ToString() =>
            $"{this.FirstName},{this.LastName},{this.Title},{this.Department},{this.Project},{this.Avatar},{this.EmployeeId}";
    }
}