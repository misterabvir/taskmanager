using Domain;
using System.Threading.Tasks;
using DAL.Base;
using System.Collections.Generic;
using System;

namespace DAL;

public class CommentsDAL: IRepository<CommentModel>
{
    private readonly IDataAccess<CommentModel> dataAccessStrategy;
    public CommentsDAL(IDataAccess<CommentModel> dataAccessStrategy)
    {
        this.dataAccessStrategy = dataAccessStrategy;
    }

    public async Task AddAsync(CommentModel entity)
    {
        await dataAccessStrategy.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await dataAccessStrategy.DeleteAsync(id);
    }

    public async Task<IEnumerable<CommentModel>> GetAllAsync()
    {
        return await dataAccessStrategy.GetAllAsync();
    }

    public async Task<CommentModel> GetByIdAsync(Guid id)
    {
        return await dataAccessStrategy.GetByIdAsync(id);
    }

    public async Task UpdateAsync(CommentModel entity)
    {
        await dataAccessStrategy.UpdateAsync(entity);
    }
}