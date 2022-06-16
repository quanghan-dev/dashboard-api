using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IConfigsRepository Configs { get; }
        IDashboardRepository Dashboards { get; }
        IContactRepository Contacts { get; }
        ITaskRepository Tasks { get; }
        IWidgetRepository Widgets { get; }
        Task SaveChangesAsync();
    }
}