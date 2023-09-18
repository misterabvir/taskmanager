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
        ProjectModel project = await projects.GetById(projectId);
        TaskModel model = new TaskModel() {
            Id = Guid.NewGuid(),
            TaskName = taskName,
            ProjectId = project.Id,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };
        await projects.Update(project.Id);
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
            await projects.Update(model.ProjectId);
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
        await projects.Update(model.ProjectId);
        return model;
    }

    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        return await tasksDAL.GetAll();
    }

    public async Task<IEnumerable<TaskModel>> GetByProjectId(Guid projectId)
    {
        return await tasksDAL.GetByProjectId(projectId);
    }

    public async Task<TaskModel> UpdateTime(Guid taskId)
    {
        TaskModel model = await tasksDAL.GetById(taskId);
        model.UpdateDate = DateTime.UtcNow;
        await tasksDAL.Update(model);
        await projects.Update(model.ProjectId);
        return model;
    }

    public async Task<TaskModel> GetById(Guid taskId)
    {
        return await tasksDAL.GetById(taskId);
    }
}




