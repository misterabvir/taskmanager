using Domain;
using System.Threading.Tasks;
using DAL.DapperAccess.Base;
using DAL.Base;
using System.Collections.Generic;
using System;

namespace DAL.DapperAccess;

public class CommentsDapperDAL : IDataAccess<CommentModel>
{
    private readonly IExecuteService service;
    public CommentsDapperDAL(IExecuteService service)
    {
        this.service = service;
    }

    public async Task AddAsync(CommentModel model)
    {
        await service.ExecuteAsync(SQL.Comments.Create, model);
    }

    public async Task DeleteAsync(Guid commentId)
    {
        await service.ExecuteAsync(SQL.Comments.Delete, new { CommentId = commentId });
    }

    public Task<IEnumerable<CommentModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CommentModel> GetByIdAsync(Guid commentId)
    {
        return await service.QuerySingleAsync<CommentModel>(SQL.Comments.GetById, new { CommentId = commentId });
    }

    public async Task UpdateAsync(CommentModel model)
    {
        await service.ExecuteAsync(SQL.Comments.Update, model);
    }
}