using DAL.Base;
using DAL.EFAccess.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EFAccess;

public class TaskEFDal : IDataAccess<TaskModel>
{
    private readonly TaskManagerDbContext context;
    public TaskEFDal(TaskManagerDbContext context)
    {
        this.context = context;
    }
    public async Task AddAsync(TaskModel entity)
    {
        DbModels.Task model = TaskMapper.MapTaskModelToTaskDbModel(entity);
        await context.Tasks.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public Task DeleteAsync(TaskModel entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        var dbModels = await context.Tasks.ToListAsync();
        foreach (var model in dbModels)
        {
            await context.Entry(model).Collection(m => m.Comments).LoadAsync();
        }
        return dbModels.Select(TaskMapper.MapTaskDbModelToTaskModel);
    }

    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        DbModels.Task model = context.Tasks.First(p => p.TaskId == id.ToString());
        await context.Entry(model).Collection(m => m.Comments).LoadAsync();
        return TaskMapper.MapTaskDbModelToTaskModel(model);
    }

    public async Task UpdateAsync(TaskModel entity)
    {
        DbModels.Task model = context.Tasks.First(p => p.TaskId == entity.TaskId.ToString());
        model.UpdateDate = entity.UpdateDate;
        model.TaskName = entity.TaskName;
        model.UpdateDate = entity.UpdateDate;
        model.CancelDate = entity.CancelDate;
        model.StartDate = entity.StartDate;
        model.Description = entity.Description;
        await context.SaveChangesAsync();
    }
}
