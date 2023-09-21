using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DAL.DapperAccess.Base;
using DAL.Base;

namespace DAL.DapperAccess;

public class TasksDapperDAL : IDataAccess<TaskModel>
{
    private readonly IExecuteService repository;
    public TasksDapperDAL(IExecuteService repository)
    {
        this.repository = repository;
    }

    public async Task AddAsync(TaskModel model)
    {
        await repository.ExecuteAsync(SQL.Task.Create, model);
    }

    public async Task UpdateAsync(TaskModel model)
    {
        await repository.ExecuteAsync(SQL.Task.Update, model);
    }

    public async Task<TaskModel> GetByIdAsync(Guid id)
    {
        TaskModel model = null;
        var results = await repository.QueryAsync<TaskModel, CommentModel>(
            sql: SQL.Task.GetByTaskId,
            map: (task, comment) =>
            {
                if (model == null)
                {
                    model = task;
                    model.Comments = new List<CommentModel>();
                }
                if (comment != null)
                    model.Comments.Add(comment);
                return model;
            },
            splitOn: SQL.Task.SplitOnByCommentId,
            model: new { TaskId = id });
        return model;
    }

    public Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TaskModel entity)
    {
        throw new NotImplementedException();
    }
}