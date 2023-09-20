using DAL;
using Domain;

namespace BL;

public class Comments : IComments
{

    private readonly ICommentsDAL commentsDAL;
    public Comments(ICommentsDAL commentsDAL)
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

        await commentsDAL.Create(model);
        return  model;
    }
}
