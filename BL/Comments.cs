using DAL;
using Domain;

namespace BL;

public class Comments : IComments
{
    private readonly ITasks tasks;
    private readonly ICommentsDAL commentsDAL;
    public Comments(ITasks tasks, ICommentsDAL commentsDAL)
    {
        this.tasks = tasks;
        this.commentsDAL = commentsDAL;
    }

    public async Task<CommentModel> Create(Guid taskId, int commentType, string content)
    {
        CommentModel model = new CommentModel()
        {
            Id = Guid.NewGuid(),
            TaskId = taskId,
            CommentType = commentType,
            Content = content
        };

        await commentsDAL.Create(model);
        await tasks.UpdateTime(taskId);
        return  model;
    }

    public async Task<IEnumerable<CommentModel>> GetByTaskId(Guid taskId)
    {
        return await commentsDAL.GetByTaskId(taskId);
    }
}
