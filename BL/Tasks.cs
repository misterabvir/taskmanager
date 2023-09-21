using DAL;
using DAL.Base;
using Domain;

namespace BL;

public class Tasks: ITasks
{
    private readonly IRepository<TaskModel> tasksDAL;
    public Tasks(IRepository<TaskModel> tasksDAL)
    {
        this.tasksDAL = tasksDAL;
    }

    public async Task<TaskModel?> Create(string taskName, Guid projectId)
    {
        TaskModel model = new TaskModel() {
            TaskId = Guid.NewGuid(),
            TaskName = taskName,
            ProjectId = projectId,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };
        await tasksDAL.AddAsync(model);
        return model;
    }

    public async Task<TaskModel> Start(Guid taskId)
    {
        TaskModel model = await tasksDAL.GetByIdAsync(taskId);
        if (model.StartDate == null)
        {
            model.StartDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            await tasksDAL.UpdateAsync(model);
        }
        return model;
    }

    public async Task<TaskModel> Cancel(Guid taskId)
    {
        TaskModel model = await tasksDAL.GetByIdAsync(taskId);
        if (model.StartDate != null && model.CancelDate == null)
        {
            model.CancelDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            await tasksDAL.UpdateAsync(model);
        }
        return model;
    }
    public async Task<TaskModel> UpdateName(Guid taskId, string taskName)
    {
        TaskModel model = await tasksDAL.GetByIdAsync(taskId);
        model.TaskName = taskName;
        model.UpdateDate = DateTime.Now;
        await tasksDAL.UpdateAsync(model);
        return model;
    }

    public async Task<TaskModel> GetById(Guid taskId)
    {
        return await tasksDAL.GetByIdAsync(taskId);
    }

    public async Task UpdateDescription(Guid taskId, string description)
    {
        TaskModel model = await tasksDAL.GetByIdAsync(taskId);
        model.UpdateDate = DateTime.UtcNow;
        model.Description = description;
        await tasksDAL.UpdateAsync(model);
    }
}




