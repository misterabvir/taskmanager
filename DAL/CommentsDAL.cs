using Domain;
using System.Threading.Tasks;
using System;
using DAL.Base;
using System.Collections.Generic;

namespace DAL;

public class CommentsDAL : ICommentsDAL
{
    private readonly IRepository repository;
    public CommentsDAL(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(CommentModel model)
    {
        await repository.ExecuteAsync(SQL.Comments.Create, model);
    }

    public async Task<IEnumerable<CommentModel>> GetByTaskId(Guid taskId)
    {
        return await repository.QueryAsync<CommentModel>(SQL.Comments.GetById, new { TaskId = taskId });
    }

}