using Domain;

namespace BL;

public interface IComments
{
    Task<CommentModel> Create(Guid taskId, string content);
}
