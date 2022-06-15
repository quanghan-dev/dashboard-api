namespace Core.Entities {
  public class Dashboard {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? LayoutType { get; set; }
    public Configs? Configs { get; set; }

    public Account? Account { get; set; }
    public ICollection<Widget>? Widgets { get; set; }
  }
}