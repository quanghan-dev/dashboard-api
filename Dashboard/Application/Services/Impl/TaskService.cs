using Application.Exceptions;
using Application.Models;
using Application.Models.Tasks;
using AutoMapper;
using Core.Enums;
using DataAccess.UnitOfWork;

namespace Application.Services.Impl
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUtilService _utilService;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork unitOfWork, IUtilService utilService,
        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _utilService = utilService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<TaskResponse>> CreateTask(string userId, TaskRequest request)
        {
            try
            {
                Core.Entities.Task task = _mapper.Map<Core.Entities.Task>(request);
                task.Id = Guid.NewGuid();
                task.UserId = Guid.Parse(userId);
                task.Status = (int)Status.Active;

                _unitOfWork.Tasks.Add(task);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<TaskResponse>.Success(_mapper.Map<TaskResponse>(task));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Delete Task By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteTaskById(string userId, Guid id)
        {
            try
            {
                Core.Entities.Task? task = await _unitOfWork.Tasks
                    .FindAsync(t => t.UserId.Equals(Guid.Parse(userId)) && t.Id.Equals(id));

                if (task is null)
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                task.Status = (int)Status.Deleted;

                _unitOfWork.Tasks.Update(task);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<string>.Success(Message.GetMessage(ServiceMessage.Successful));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<TaskResponse>> GetTaskById(string userId, Guid id)
        {
            try
            {
                Core.Entities.Task? task = await _unitOfWork.Tasks
                    .FindAsync(t => t.UserId.Equals(Guid.Parse(userId)) && t.Id.Equals(id) && t.Status.Equals(Status.Active));

                if (task is null)
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                return ApiResult<TaskResponse>.Success(_mapper.Map<TaskResponse>(task));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Get Tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<TaskResponse>>> GetTasks(string userId)
        {
            try
            {
                List<Core.Entities.Task>? tasks = await _unitOfWork.Tasks
                    .FindListAsync(t => t.UserId.Equals(Guid.Parse(userId)) && t.Status.Equals(Status.Active));

                if (!tasks.Any())
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                return ApiResult<List<TaskResponse>>.Success(_mapper.Map<List<TaskResponse>>(tasks));
            }
            catch (System.Exception) { throw; }
        }

        /// <summary>
        /// Update Task By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<TaskResponse>> UpdateTaskById(string userId, Guid id, TaskRequest request)
        {
            try
            {
                Core.Entities.Task? task = await _unitOfWork.Tasks
                    .FindAsync(t => t.UserId.Equals(Guid.Parse(userId)) && t.Id.Equals(id));

                if (task is null)
                    throw new NotFoundException(Message.GetMessage(ErrorMessage.Resource_Not_Found));

                task.TaskName = request.TaskName is null ? task.TaskName : request.TaskName;
                task.IsCompleted = request.IsCompleted is null ? task.IsCompleted : request.IsCompleted.Value;

                _unitOfWork.Tasks.Update(task);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<TaskResponse>.Success(_mapper.Map<TaskResponse>(task));
            }
            catch (System.Exception) { throw; }
        }
    }
}