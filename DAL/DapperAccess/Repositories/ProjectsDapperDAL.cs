using DAL.Base;
using DAL.DapperAccess.Base;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DapperAccess;

public class ProjectsDapperDAL : IDataAccess<ProjectModel>
{
    private readonly IExecuteService repository;
    public ProjectsDapperDAL(IExecuteService repository)
    {
        this.repository = repository;
    }

    public async Task AddAsync(ProjectModel model)
    {
        await repository.ExecuteAsync(SQL.Project.Create, model);
    }

    public Task DeleteAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProjectModel>> GetAllAsync()
    {
        var projectList = new List<ProjectModel>();
        var results = await repository.QueryAsync<ProjectModel, TaskModel>(
            sql: SQL.Project.GetAll,
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

    public async Task<ProjectModel> GetByIdAsync(Guid id)
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

    public async Task UpdateAsync(ProjectModel model)
    {
        await repository.ExecuteAsync(SQL.Project.Update, model);
    }
}
