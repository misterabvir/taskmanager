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
}
