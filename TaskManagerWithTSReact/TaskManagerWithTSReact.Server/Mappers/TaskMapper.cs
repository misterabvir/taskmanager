using Domain;
using TaskManagerWithTSReact.Server.ViewModels;

namespace TaskManagerWithTSReact.Server.Mappers
{
    public static class TaskMapper
    {
        public static TaskViewModel MapTaskModelToTaskViewModel(TaskModel model)
        {
            return new TaskViewModel()
            {
                TaskId = model.TaskId,
                TaskName = model.TaskName,
                Description = model.Description ?? "",
                ProjectName = model.ProjectName,
                ProjectId = model.ProjectId,
                StartDate = model.StartDate,
                UpdateDate = model.UpdateDate,
                CancelDate = model.CancelDate,
                CreateDate = model.CreateDate,
                Comments = model.Comments
                    .Select(CommentMapper.MapCommentModelToCommentViewModel)
                    .OrderByDescending(c => c.Created)
                    .ToList(),
            };
        }
    }
}
