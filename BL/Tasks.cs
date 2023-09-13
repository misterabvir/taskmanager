﻿using DAL;
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

    public async Task<TaskModel?> Create(string taskName, string projectName)
    {
        ProjectModel project = await projects.GetByName(projectName);
        TaskModel model = new TaskModel() {
            Id = Guid.NewGuid(),
            ProjectId = project.Id,
            CreateDate = DateTime.UtcNow
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
            model.StartDate = DateTime.UtcNow;
            model.UpdateDate = DateTime.UtcNow;
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
            model.CancelDate = DateTime.UtcNow;
            model.UpdateDate = DateTime.UtcNow;
            await tasksDAL.Update(model);
        }
        return model;
    }
    public async Task<TaskModel> UpdateName(Guid taskId, string taskName)
    {
        TaskModel model = await tasksDAL.GetById(taskId);
        model.TaskName = taskName;
        model.UpdateDate = DateTime.UtcNow;
        await projects.Update(model.ProjectId);
        return model;
    }
    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        return await tasksDAL.GetAll();
    }
    public async Task<IEnumerable<TaskModel>> GetByProjectName(string projectName)
    {
        ProjectModel project = await projects.GetByName(projectName);
        if (project == null) return Enumerable.Empty<TaskModel>();
        return await GetByProjectId(project.Id);
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



