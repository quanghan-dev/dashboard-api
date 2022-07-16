using Application.Exceptions;
using Application.Models.Dashboards;
using Application.Models.Widgets;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using DataAccess.UnitOfWork;

namespace Application.Services.Impl
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Dashboards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of dashboard</returns>
        public async Task<List<DashboardResponse>> GetDashboards(Guid? userId)
        {
            try
            {
                List<Dashboard> dashboards = await _unitOfWork.Dashboards.GetDashboards(userId);
                if (!dashboards.Any())
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                return _mapper.Map<List<DashboardResponse>>(dashboards);
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Update Dashboard By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>dashboard</returns>
        public async Task<DashboardResponse> UpdateDashboardById(Guid? userId, Guid id, UpdateDashboardRequest request)
        {
            try
            {
                Dashboard? dashboard = await _unitOfWork.Dashboards.FindAsync(d => d.UserId.Equals(userId) && d.Id.Equals(id));

                if (dashboard is not null)
                {
                    dashboard.Title = request.Title == null ? dashboard.Title : request.Title;
                    dashboard.LayoutType = request.LayoutType == null ? dashboard.LayoutType : request.LayoutType;
                }


                List<Widget> removedWidgets = new();
                List<WidgetDto> widgetDtos = request.Widgets!;
                if (dashboard!.Widgets != null && dashboard.Widgets.Any())
                {
                    removedWidgets = dashboard.Widgets.ToList();
                    foreach (var widget in dashboard.Widgets)
                    {
                        if (widgetDtos.Any())
                        {
                            var widgetDto = widgetDtos.Find(w => w.WidgetId.Equals(widget.WidgetId));
                            if (widgetDto != null)
                            {
                                removedWidgets.Remove(widget);
                                widgetDtos.Remove(widgetDto);
                                _mapper.Map<WidgetDto, Widget>(widgetDto, widget);
                                widget.DashboardId = dashboard.Id;
                                _unitOfWork.Widgets.Update(widget);
                            }
                        }
                    }
                }

                if (removedWidgets.Any())
                {
                    foreach (var removedWidget in removedWidgets)
                    {
                        Widget widget = dashboard.Widgets!.Where(w => w.WidgetId.Equals(removedWidget.WidgetId)).First();
                        _unitOfWork.Widgets.Delete(widget);
                    }
                }
                if (widgetDtos.Any())
                {
                    foreach (var widgetDto in widgetDtos)
                    {
                        widgetDto.WidgetId ??= Guid.NewGuid();
                        Widget widget = _mapper.Map<Widget>(widgetDto);
                        widget.DashboardId = dashboard.Id;
                        _unitOfWork.Widgets.Add(widget);
                    }
                }

                _unitOfWork.Dashboards.Update(dashboard);


                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<DashboardResponse>(dashboard);
            }
            catch (System.Exception) { throw; }
        }
    }
}