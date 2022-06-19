using Application.Models;
using Application.Models.Tasks;

namespace Application.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<TaskResponse>> CreateTask(string userId, TaskRequest request);

        /// <summary>
        /// Get sTasks
        /// </summary>        
        /// <param name="userId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<ApiResult<List<TaskResponse>>> GetTasks(string userId, string? keyword);

        /// <summary>
        /// Get Task By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<TaskResponse>> GetTaskById(string userId, Guid id);

        /// <summary>
        /// Update Task By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<TaskResponse>> UpdateTaskById(string userId, Guid id, TaskRequest request);

        /// <summary>
        /// Delete Task By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteTaskById(string userId, Guid id);

    }
}