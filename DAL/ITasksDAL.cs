using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Security.Permissions;

namespace DAL;

public interface ITasksDAL
{
    /// <summary> create new row in `Task` table </summary>
    Task Create(TaskModel model);

    /// <summary> update row in `Task` table </summary>
    Task Update(TaskModel model);

    /// <summary> get all rows from `Task` table </summary>
    Task<IEnumerable<TaskModel>> GetAll();

    /// <summary> get row by id from `Task` table </summary>
    Task<TaskModel> GetById(Guid id);

    /// <summary> get row by projectId from `Task` table </summary>
    Task<IEnumerable<TaskModel>> GetByProjectId(Guid projectId);
}
