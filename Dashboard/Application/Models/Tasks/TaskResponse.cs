namespace Application.Models.Tasks
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string? TaskName { get; set; }
        public bool IsCompleted { get; set; }
    }
}