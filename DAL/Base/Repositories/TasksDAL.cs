using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DAL.Base;

namespace DAL;
public class TasksDAL : IRepository<TaskModel>
{
    private readonly IDataAccess<TaskModel> dataAccessStrategy;
    public TasksDAL(IDataAccess<TaskModel> dataAccessStrategy)
    {
        this.dataAccessStrategy = dataAccessStrategy;
    }

    public async Task AddAsync(TaskModel entity)
    {
        await dataAccessStrategy.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await dataAccessStrategy.DeleteAsync(id);
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        return await dataAccessStrategy.GetAllAsync();
    }

    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        return await dataAccessStrategy.GetByIdAsync(id);
    }

    public async Task UpdateAsync(TaskModel entity)
    {
        await dataAccessStrategy.UpdateAsync(entity);
    }
}