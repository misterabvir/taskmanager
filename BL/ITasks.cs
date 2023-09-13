using Domain;

namespace BL;

public interface ITasks
{
    Task<TaskModel?> Create(string taskName, string projectName);
    Task<TaskModel> Start(Guid taskId);
    Task<TaskModel> Cancel(Guid taskId);
    Task<TaskModel> UpdateName(Guid taskId, string taskName);
    Task<TaskModel> UpdateTime(Guid taskId);
    Task<IEnumerable<TaskModel>> GetAll();
    Task<IEnumerable<TaskModel>> GetByProjectName(string projectName);
    Task<IEnumerable<TaskModel>> GetByProjectId(Guid projectId);
    Task<TaskModel> GetById(Guid taskId);
}




