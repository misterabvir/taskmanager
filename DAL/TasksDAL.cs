using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DAL.Base;
using System.Linq;

namespace DAL;

public class TasksDAL : ITasksDAL
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

    public async Task<TaskModel> GetById(Guid id)
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
}