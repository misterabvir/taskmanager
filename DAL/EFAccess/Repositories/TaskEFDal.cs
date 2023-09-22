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
    public async Task AddAsync(TaskModel model)
    {
        DbModels.Task task = TaskMapper.MapTaskModelToTaskDbModel(model);
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        var tasks = await context.Tasks.ToListAsync();
        tasks
            .ForEach(async (task) => await context
            .Entry(task)
            .Collection(t => t.Comments)
            .LoadAsync());
        return tasks.Select(TaskMapper.MapTaskDbModelToTaskModel);
    }

    public async Task<TaskModel> GetByIdAsync(Guid taskId)
    {
        DbModels.Task model = context.Tasks.First(p => p.TaskId == taskId);
        await context
            .Entry(model)
            .Collection(m => m.Comments)
            .LoadAsync();
        return TaskMapper.MapTaskDbModelToTaskModel(model);
    }

    public async Task UpdateAsync(TaskModel model)
    {
        DbModels.Task task = await context.Tasks.FirstAsync(t => t.TaskId == model.TaskId);
        task.UpdateDate = model.UpdateDate;
        task.TaskName = model.TaskName;
        task.UpdateDate = model.UpdateDate;
        task.CancelDate = model.CancelDate;
        task.StartDate = model.StartDate;
        task.Description = model.Description;
        DbModels.Project project = await context.Projects.FirstAsync(p => p.ProjectId == task.ProjectId);
        project.UpdateDate = model.UpdateDate;
        await context.SaveChangesAsync();
    }
}
