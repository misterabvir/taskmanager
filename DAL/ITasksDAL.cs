using Domain;
using System.Threading.Tasks;
using System;

namespace DAL;

public interface ITasksDAL
{
    Task Create(TaskModel model);

    Task Update(TaskModel model);

    Task<TaskModel> GetById(Guid id);
}
