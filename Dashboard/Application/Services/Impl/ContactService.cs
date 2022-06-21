using System.Text;
using Application.Exceptions;
using Application.Models;
using Application.Models.Contacts;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Impl
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilService _utilService;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IUtilService utilService,
        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _utilService = utilService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ContactResponse>> CreateContact(CreateContactRequest request)
        {
            try
            {
                Contact contact = _mapper.Map<Contact>(request);
                contact.Id = Guid.NewGuid();
                contact.Status = (int)Status.Active;

                _unitOfWork.Contacts.Add(contact);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<ContactResponse>.Success(_mapper.Map<ContactResponse>(contact));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Delete Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteContactById(Guid id)
        {
            try
            {
                Contact? contact = await _unitOfWork.Contacts
                    .FindAsync(c => c.Id.Equals(id));

                if (contact is null)
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                contact.Status = (int)Status.Deleted;

                _unitOfWork.Contacts.Update(contact);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<string>.Success(Message.GetMessage(ServiceMessage.Successful));
            }
            catch (System.Exception) { throw; }
        }


        /// <summary>
        /// Export Contacts
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExportContacts()
        {
            try
            {
                List<Contact>? contacts = await _unitOfWork.Contacts.FindListAsync(contact => contact.Id != Guid.Empty);

                StringBuilder csv = new();
                string headerLine = $"{nameof(Contact.FirstName)},{nameof(Contact.LastName)},{nameof(Contact.Title)}," +
                                    $"{nameof(Contact.Department)},{nameof(Contact.Project)},{nameof(Contact.Project)}," +
                                    $"{nameof(Contact.Avatar)},{nameof(Contact.EmployeeId)}";
                csv.AppendLine(headerLine);

                foreach (var contact in contacts)
                {
                    csv.AppendLine(contact.ToString());
                }
                return csv.ToString();
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Get Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<ContactResponse>> GetContactById(Guid id)
        {
            try
            {
                Contact? contact = await _unitOfWork.Contacts
                    .FindAsync(c => c.Id.Equals(id) && c.Status.Equals(Status.Active));

                if (contact is null)
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                return ApiResult<ContactResponse>.Success(_mapper.Map<ContactResponse>(contact));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Get Contacts
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ContactResponse>>> GetContacts(string? keyword)
        {
            try
            {
                List<Contact>? contacts = await _unitOfWork.Contacts.GetContactsByKeyword(keyword);

                if (!contacts.Any())
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                return ApiResult<List<ContactResponse>>.Success(_mapper.Map<List<ContactResponse>>(contacts));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Import Contacts
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ContactResponse>>> ImportContacts(IFormFile file)
        {
            List<Contact> contacts = new();
            try
            {
                if (file.Length > 0)
                {
                    using var fileStream = file.OpenReadStream();
                    using var reader = new StreamReader(fileStream);

                    string headerLine = reader.ReadLine()!;
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Contact contact = Contact.FromCsv(line);
                        contact.Id = Guid.NewGuid();
                        contact.Status = (int)Status.Active;

                        _unitOfWork.Contacts.Add(contact);
                        contacts.Add(contact);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    return ApiResult<List<ContactResponse>>.Success(_mapper.Map<List<ContactResponse>>(contacts));
                }
                else throw new BadRequestException(Message.GetMessage(ErrorMessage.Invalid_File));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Update Contact By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ContactResponse>> UpdateContactById(Guid id, UpdateContactRequest request)
        {
            try
            {
                Contact? contact = await _unitOfWork.Contacts
                    .FindAsync(c => c.Id.Equals(id));

                if (contact is null)
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                // contact.FirstName = request.FirstName is null ? contact.FirstName : request.FirstName;
                // contact.LastName = request.LastName is null ? contact.LastName : request.LastName;
                // contact.Title = request.Title is null ? contact.Title : request.Title;
                // contact.Department = request.Department is null ? contact.Department : request.Department;
                // contact.Project = request.Project is null ? contact.Project : request.Project;
                // contact.Avatar = request.Avatar is null ? contact.Avatar : request.Avatar;

                contact = _mapper.Map<Contact>(request);

                _unitOfWork.Contacts.Update(contact);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<ContactResponse>.Success(_mapper.Map<ContactResponse>(contact));
            }
            catch (System.Exception) { throw; }
        }
    }
}