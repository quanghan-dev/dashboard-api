using Application.Models.Widgets;

namespace Application.Models.Dashboards
{
    public class UpdateDashboardRequest
    {
        public string? Title { get; set; }
        public string? LayoutType { get; set; }
        public List<WidgetDto>? Widgets { get; set; }
    }
}