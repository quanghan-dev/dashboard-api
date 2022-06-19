namespace Core.Entities
{
    public class Widget
    {
        public Guid WidgetId { get; set; }
        public string? Title { get; set; }
        public string? WidgetType { get; set; }
        public int? MinWidth { get; set; }
        public int? minHeight { get; set; }
        public int Status { get; set; }

        public Configs? Configs { get; set; }
    }
}