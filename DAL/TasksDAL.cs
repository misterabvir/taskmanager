using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DAL.Base;

namespace DAL;

public class TasksDAL:ITasksDAL
{
    private readonly IRepository repository;
    public TasksDAL(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(TaskModel model)
    {
        await repository.ExecuteAsync(SQL.Task.Create, model);
    }
    public async Task Update(TaskModel model)
    {
        await repository.ExecuteAsync(SQL.Task.Update, model);
    }
    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        return await repository.QueryAsync<TaskModel>(SQL.Task.GetAll, null);
    }
    public async Task<TaskModel> GetById(Guid id)
    {
        return await repository.QuerySingleOrDefaultAsync<TaskModel>(SQL.Task.GetAll, new { Id = id });
    }
    public async Task<IEnumerable<TaskModel>> GetByProjectId(Guid projectId)
    {
        return await repository.QueryAsync<TaskModel>(SQL.Task.GetAll, new { ProjectId = projectId });
    }
}