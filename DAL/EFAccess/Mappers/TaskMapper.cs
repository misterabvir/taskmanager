using DAL.EFAccess;
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
            TaskId = model.TaskId.ToString(),
            ProjectId = model.ProjectId.ToString(),
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
            TaskId = Guid.Parse(model.TaskId),
            ProjectId = Guid.Parse(model.ProjectId),
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

internal static class ProjectMapper
{
    public static DbModels.Project MapProjectModelToProjectDbModel(ProjectModel model)
    {
        return new DbModels.Project()
        {
            ProjectId = model.ProjectId.ToString().ToLower(),
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
        };
    }

    public static ProjectModel MapProjectDbModelToProjectModel(DbModels.Project model)
    {
        return new ProjectModel()
        {
            ProjectId = Guid.Parse(model.ProjectId),
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate.Value,
            UpdateDate = model.UpdateDate,
            Tasks = model.Tasks?.Select(TaskMapper.MapTaskDbModelToTaskModel).ToList() ?? new System.Collections.Generic.List<TaskModel>()
        };
    }
}

internal static class CommentMapper
{
    public static DbModels.Comment MapCommentModelToCommentDbModel(CommentModel model)
    {
        return new DbModels.Comment()
        {
            CommentId = model.CommentId.ToString().ToLower(),
            TaskId = model.TaskId.ToString(),
            Content = model.Content,
            CreateDate = model.CreateDate,        
        };
    }

    public static CommentModel MapCommentDbModelToCommentModel(DbModels.Comment model)
    {
        return new CommentModel()
        {
            CommentId = Guid.Parse(model.CommentId),
            TaskId = Guid.Parse(model.TaskId),
            Content = model.Content,
            CreateDate = model.CreateDate.Value,
        };
    }
}

