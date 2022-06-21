using Core.Entities;

namespace DataAccess.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        /// <summary>
        /// Get Contacts By Keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<Contact>> GetContactsByKeyword(string? keyword);
    }
}