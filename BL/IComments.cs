using DAL;
using Domain;

namespace BL;

public interface IComments
{
    Task<CommentModel> Create(Guid taskId, int commentType, string content);
    Task<IEnumerable<CommentModel>> GetByTaskId(Guid taskId);
}

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
        return commentsDAL.GetByTaskId(taskId);
    }
}
