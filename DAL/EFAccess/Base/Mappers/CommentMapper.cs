using Domain;
using System;

namespace DAL.EFAccess.Mappers;

internal static class CommentMapper
{
    public static DbModels.Comment MapCommentModelToCommentDbModel(CommentModel model)
    {
        return new DbModels.Comment()
        {
            CommentId = model.CommentId,
            TaskId = model.TaskId,
            Content = model.Content,
            CreateDate = model.CreateDate,        
        };
    }

    public static CommentModel MapCommentDbModelToCommentModel(DbModels.Comment model)
    {
        return new CommentModel()
        {
            CommentId = model.CommentId,
            TaskId = model.TaskId,
            Content = model.Content,
            CreateDate = model.CreateDate.Value,
        };
    }
}

