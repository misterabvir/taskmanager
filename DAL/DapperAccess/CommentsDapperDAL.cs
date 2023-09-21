using Domain;
using System.Threading.Tasks;
using DAL.DapperAccess.Base;
using DAL.Base;
using System.Collections.Generic;
using System;

namespace DAL.DapperAccess;

public class CommentsDapperDAL : IDataAccess<CommentModel>
{
    private readonly IExecuteService repository;
    public CommentsDapperDAL(IExecuteService repository)
    {
        this.repository = repository;
    }

    public async Task AddAsync(CommentModel model)
    {
        await repository.ExecuteAsync(SQL.Comments.Create, model);
    }

    public Task DeleteAsync(CommentModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CommentModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CommentModel> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CommentModel entity)
    {
        throw new NotImplementedException();
    }
}