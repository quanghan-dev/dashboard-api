using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Application.Models.Widgets
{
    public class WidgetDto
    {
        public Guid? WidgetId { get; set; }
        public string? Title { get; set; }
        public string? WidgetType { get; set; }
        public int? MinWidth { get; set; }
        public int? minHeight { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        public string? Configs { get; set; }
    }
}