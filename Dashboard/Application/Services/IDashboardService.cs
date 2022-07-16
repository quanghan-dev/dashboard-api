using Application.Models.Dashboards;

namespace Application.Services
{
    public interface IDashboardService
    {
        /// <summary>
        /// Get Dashboards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of dashboard</returns>
        public Task<List<DashboardResponse>> GetDashboards(Guid? userId);

        /// <summary>
        /// Update Dashboard By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>dashboard</returns>
        public Task<DashboardResponse> UpdateDashboardById(Guid? userId, Guid id, UpdateDashboardRequest request);
    }
}