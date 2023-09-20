using DAL;
using Domain;

namespace BL;

public class Tasks: ITasks
{
    private readonly IProjects projects;
    private readonly ITasksDAL tasksDAL;
    public Tasks(IProjects projects, ITasksDAL tasksDAL)
    {
        this.projects = projects;
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
        await tasksDAL.Create(model);
        return model;
    }

    public async Task<TaskModel> Start(Guid taskId)
    {
        TaskModel model = await tasksDAL.GetById(taskId);
        if (model.StartDate == null)
        {
            model.StartDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            await tasksDAL.Update(model);
        }
        return model;
    }

    public async Task<TaskModel> Cancel(Guid taskId)
    {
        TaskModel model = await tasksDAL.GetById(taskId);
        if (model.StartDate != null && model.CancelDate == null)
        {
            model.CancelDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            await tasksDAL.Update(model);
        }
        return model;
    }
    public async Task<TaskModel> UpdateName(Guid taskId, string taskName)
    {
        TaskModel model = await tasksDAL.GetById(taskId);
        model.TaskName = taskName;
        model.UpdateDate = DateTime.Now;
        await tasksDAL.Update(model);
        return model;
    }

    public async Task<TaskModel> GetById(Guid taskId)
    {
        return await tasksDAL.GetById(taskId);
    }

    public async Task UpdateDescription(Guid taskId, string description)
    {
        TaskModel model = await tasksDAL.GetById(taskId);
        model.UpdateDate = DateTime.UtcNow;
        model.Description = description;
        await tasksDAL.Update(model);
    }
}




