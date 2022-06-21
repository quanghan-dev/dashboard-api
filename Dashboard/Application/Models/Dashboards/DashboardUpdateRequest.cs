using Core.Entities;

namespace Application.Models.Dashboards
{
    public class DashboardUpdateRequest
    {
        public string? Title { get; set; }
        public string? LayoutType { get; set; }
        public List<Widget>? Widgets { get; set; }
    }
}