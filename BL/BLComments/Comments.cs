using DAL.Base;
using Domain;

namespace BL;

public class Comments : IComments
{

    private readonly IRepository<CommentModel> commentsDAL;
    public Comments(IRepository<CommentModel> commentsDAL)
    {
        this.commentsDAL = commentsDAL;
    }

    public async Task<CommentModel> Create(Guid taskId, string content)
    {
        CommentModel model = new CommentModel()
        {
            CommentId = Guid.NewGuid(),
            TaskId = taskId,
            CreateDate = DateTime.Now,
            Content = content
        };

        await commentsDAL.AddAsync(model);
        return  model;
    }

    public async Task Delete(Guid commentId)
    {
        await commentsDAL.DeleteAsync(commentId);
    }

    public async Task Update(Guid commentId, string content)
    {
        CommentModel model = await commentsDAL.GetByIdAsync(commentId);
        model.Content = content;
        await commentsDAL.UpdateAsync(model);
    }
}
