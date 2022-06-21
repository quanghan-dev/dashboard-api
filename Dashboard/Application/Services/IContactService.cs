using Application.Models;
using Application.Models.Contacts;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface IContactService
    {
        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ContactResponse>> CreateContact(CreateContactRequest request);

        /// <summary>
        /// Get Contact
        /// </summary>        
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<ApiResult<List<ContactResponse>>> GetContacts(string? keyword);

        /// <summary>
        /// Get Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<ContactResponse>> GetContactById(Guid id);

        /// <summary>
        /// Update Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ContactResponse>> UpdateContactById(Guid id, UpdateContactRequest request);

        /// <summary>
        /// Delete Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteContactById(Guid id);

        /// <summary>
        /// Import Contacts
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<ApiResult<List<ContactResponse>>> ImportContacts(IFormFile file);

        Task<string> ExportContacts();
    }
}