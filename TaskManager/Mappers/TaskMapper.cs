using Domain;
using TaskManager.ViewModels;

namespace TaskManager.Mappers
{
    public static class TaskMapper
    {
        public static TaskViewModel MapTaskModelToTaskViewModel(TaskModel model, string projectName)
        {
            return new TaskViewModel()
            {
                Id = model.Id,
                TaskName = model.TaskName,
                ProjectId = model.ProjectId,
                ProjectName = projectName,
                StartDate = model.StartDate,
                UpdateDate = model.UpdateDate,
                CancelDate = model.CancelDate,
                CreateDate = model.CreateDate
            };
        }
    }
}
