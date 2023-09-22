using Domain;

namespace BL;

public interface IComments
{
    Task<CommentModel> Create(Guid taskId, string content);
    Task Delete(Guid commentId);
    Task Update(Guid commentId, string content);
}
