using DAL.Base;
using Domain;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL;

public class ProjectsDAL : IProjectsDAL
{
    private readonly IRepository repository;
    public ProjectsDAL(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(ProjectModel model)
    {
        await repository.ExecuteAsync(SQL.Project.Create, model);
    }

    public async Task<IEnumerable<ProjectModel>> GetAll()
    {
        var projectList = new List<ProjectModel>();
        var results = await repository.QueryAsync<ProjectModel, TaskModel>(
            sql: SQL.Project.Get,
            map: (project, task) =>
            {
                ProjectModel model = projectList.FirstOrDefault(pr=>pr.ProjectId == project.ProjectId);
                if (model == null)
                {
                    model = project;
                    model.Tasks = new List<TaskModel>();
                    projectList.Add(model);
                }
                if(task != null)
                    model.Tasks.Add(task);
                return model;
            },
            splitOn: "TaskId");
        return projectList;
    }

    public async Task<ProjectModel> GetById(Guid id)
    {
        ProjectModel model = null;
        var results = await repository.QueryAsync<ProjectModel, TaskModel>(
            sql: SQL.Project.GetProjectByProjectId,
            map: (project, task) =>
            {
                if (model == null)
                {
                    model = project;
                    model.Tasks = new List<TaskModel>();
                }
                if (task != null)
                    model.Tasks.Add(task);
                return model;
            },
            splitOn: SQL.Project.SplitOnByTaskId,
            model: new { ProjectId = id });
        return model;
    }

    public async Task<ProjectModel> GetByName(string projectName)
    {
        ProjectModel model = null;
        var results = await repository.QueryAsync<ProjectModel, TaskModel>(
            sql: SQL.Project.GetProjectByProjectName,
            map: (project, task) =>
            {
                if (model == null)
                {
                    model = project;
                    model.Tasks = new List<TaskModel>();
                }
                if (task != null)
                    model.Tasks.Add(task);
                return model;
            },
            splitOn: SQL.Project.SplitOnByTaskId,
            model: new { ProjectName = projectName });
        return model;
    }

    public async Task Update(ProjectModel model)
    {
        await repository.ExecuteAsync(SQL.Project.Update, model);
    }


}
