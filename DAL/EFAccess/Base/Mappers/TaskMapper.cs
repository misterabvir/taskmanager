using Domain;
using System;
using System.Linq;

namespace DAL.EFAccess.Mappers;

internal static class TaskMapper
{
    public static DbModels.Task MapTaskModelToTaskDbModel(TaskModel model)
    {
        return new DbModels.Task()
        {
            TaskId = model.TaskId,
            ProjectId = model.ProjectId,
            TaskName = model.TaskName,
            Description = model.Description,
            CancelDate = model.CancelDate,
            CreateDate = model.CreateDate,
            StartDate = model.StartDate,
            UpdateDate = model.UpdateDate,
            Comments = model.Comments.Select(CommentMapper.MapCommentModelToCommentDbModel).ToList()
        };
    }

    public static TaskModel MapTaskDbModelToTaskModel(DbModels.Task model)
    {
        return new TaskModel()
        {
            TaskId =model.TaskId,
            ProjectId = model.ProjectId,
            TaskName = model.TaskName,
            ProjectName = model.Project?.ProjectName ?? "",
            Description = model.Description,
            CancelDate = model.CancelDate,
            CreateDate = model.CreateDate.Value,
            StartDate = model.StartDate,
            UpdateDate = model.UpdateDate,
            Comments = model.Comments?.Select(CommentMapper.MapCommentDbModelToCommentModel).ToList() ?? new System.Collections.Generic.List<CommentModel>()
        };
    }
}

