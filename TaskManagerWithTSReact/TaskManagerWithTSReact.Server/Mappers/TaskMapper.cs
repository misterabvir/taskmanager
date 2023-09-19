using Domain;
using TaskManager.ViewModels;

namespace TaskManager.Mappers
{
    public static class TaskMapper
    {
        public static TaskViewModel MapTaskModelToTaskViewModel(TaskModel model)
        {
            return new TaskViewModel()
            {
                TaskId = model.TaskId,
                TaskName = model.TaskName,

                ProjectId = model.ProjectId,
                StartDate = model.StartDate,
                UpdateDate = model.UpdateDate,
                CancelDate = model.CancelDate,
                CreateDate = model.CreateDate
            };
        }
    }
}
