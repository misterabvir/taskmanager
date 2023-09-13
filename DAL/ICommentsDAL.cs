using Domain;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DAL;

public interface ICommentsDAL
{
    /// <summary> create new row in `TaskComments` table </summary>
    Task Create(CommentModel model);

    /// <summary> get row by taskId from `TaskComments` table </summary>
    Task<IEnumerable<CommentModel>> GetByTaskId(Guid taskId);
}
