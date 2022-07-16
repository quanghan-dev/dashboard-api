using Application.Models.Widgets;
using Core.Entities;

namespace Application.Models.Dashboards
{
    public class DashboardResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? LayoutType { get; set; }
        public Guid UserId { get; set; }
        public List<WidgetDto>? Widgets { get; set; }
    }
}