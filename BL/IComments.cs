using Domain;

namespace BL;

public interface IComments
{
    Task<CommentModel> Create(Guid taskId, int commentType, string content);
    Task<IEnumerable<CommentModel>> GetByTaskId(Guid taskId);
}
