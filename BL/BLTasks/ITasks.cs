using Domain;

namespace BL;

public interface ITasks
{
    Task<TaskModel?> Create(string taskName, Guid projectId);
    Task<TaskModel> Start(Guid taskId);
    Task<TaskModel> Cancel(Guid taskId);
    Task<TaskModel> UpdateName(Guid taskId, string taskName);
    Task<TaskModel> GetById(Guid taskId);
    Task UpdateDescription(Guid taskId, string description);
}




