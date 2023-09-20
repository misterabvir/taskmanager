using Domain;
using TaskManagerWithTSReact.Server.ViewModels;

namespace TaskManagerWithTSReact.Server.Mappers
{
    public static class CommentMapper
    {
        public static CommentViewModel MapCommentModelToCommentViewModel(CommentModel model)
        {
            return new CommentViewModel()
            {
                Id = model.CommentId,
                TaskId = model.TaskId,
                Created = model.CreateDate,
                Content = model.Content
            };
        }
    }
}
