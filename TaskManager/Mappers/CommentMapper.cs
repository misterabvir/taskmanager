using Domain;
using TaskManager.ViewModels;

namespace TaskManager.Mappers
{
    public static class CommentMapper
    {
        public static CommentViewModel MapCommentModelToCommentViewModel(CommentModel model)
        {
            return new CommentViewModel()
            {
                Id = model.Id,
                TaskId = model.TaskId,
                Created = model.CreateDate,
                Content = model.Content
            };
        }
    }
}
