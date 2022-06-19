using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string? TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public int Status { get; set; }
        [ForeignKey("Account")]
        public Guid UserId { get; set; }

        public Account? Account { get; set; }
    }
}