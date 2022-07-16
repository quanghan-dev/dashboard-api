using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

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
        [ForeignKey("Dashboard")]
        public Guid DashboardId { get; set; }

        [Column(TypeName = "jsonb")]
        public string Configs { get; set; } = (new JsonObject { ["additionalProp1"] = "", ["additionalProp2"] = "", ["additionalProp3"] = "" }).ToJsonString();
    }
}