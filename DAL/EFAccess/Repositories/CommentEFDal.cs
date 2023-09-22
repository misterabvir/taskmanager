using DAL.Base;
using DAL.EFAccess.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EFAccess;

public class CommentEFDal : IDataAccess<CommentModel>
{
    private readonly TaskManagerDbContext context;
    public CommentEFDal(TaskManagerDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(CommentModel model)
    {
        DbModels.Comment comment = CommentMapper.MapCommentModelToCommentDbModel(model);
        await context.Comments.AddAsync(comment);
        DbModels.Task task = await context.Tasks.FirstAsync(t => t.TaskId == comment.TaskId);
        task.UpdateDate = comment.CreateDate;
        DbModels.Project project = await context.Projects.FirstAsync(p => p.ProjectId == task.ProjectId);
        project.UpdateDate = comment.CreateDate;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {      
        DbModels.Comment comment = context.Comments.First(comment => comment.CommentId == id);
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();      
    }

    public Task<IEnumerable<CommentModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CommentModel> GetByIdAsync(Guid commentId)
    {
        DbModels.Comment comment = await context.Comments.FirstAsync(c => c.CommentId == commentId);
        return CommentMapper.MapCommentDbModelToCommentModel(comment);
    }

    public async Task UpdateAsync(CommentModel model)
    {
        DbModels.Comment comment = await context.Comments.FirstAsync(c => c.CommentId == model.CommentId);
        comment.Content = model.Content;
        await context.SaveChangesAsync();
    }
}
