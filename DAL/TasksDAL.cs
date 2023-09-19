﻿using Domain;
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
        var result = await repository.QueryAsync<TaskModel>(SQL.Task.GetAll, null);
    
        return result;
    }
    public async Task<TaskModel> GetById(Guid id)
    {
        return await repository.QuerySingleOrDefaultAsync<TaskModel>(SQL.Task.GetById, new { TaskId = id });
    }
    public async Task<IEnumerable<TaskModel>> GetByProjectId(Guid projectId)
    {
        return await repository.QueryAsync<TaskModel>(SQL.Task.GetByProjectId, new { ProjectId = projectId });
    }
}