using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IDashboardRepository Dashboards { get; }
        IContactRepository Contacts { get; }
        ITaskRepository Tasks { get; }
        ITokenRepository Tokens { get; }
        Task SaveChangesAsync();
    }
}