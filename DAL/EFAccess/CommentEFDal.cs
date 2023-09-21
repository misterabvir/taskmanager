using DAL.Base;
using DAL.EFAccess.Mappers;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.EFAccess;

public class CommentEFDal : IDataAccess<CommentModel>
{
    private readonly TaskManagerDbContext context;
    public CommentEFDal(TaskManagerDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(CommentModel entity)
    {
        DbModels.Comment model = CommentMapper.MapCommentModelToCommentDbModel(entity);
        await context.Comments.AddAsync(model);
        await context.SaveChangesAsync();
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
