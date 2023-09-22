using Domain;
using System;
using System.Linq;

namespace DAL.EFAccess.Mappers;

internal static class ProjectMapper
{
    public static DbModels.Project MapProjectModelToProjectDbModel(ProjectModel model)
    {
        return new DbModels.Project()
        {
            ProjectId = model.ProjectId,
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
        };
    }

    public static ProjectModel MapProjectDbModelToProjectModel(DbModels.Project model)
    {
        return new ProjectModel()
        {
            ProjectId = model.ProjectId,
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate.Value,
            UpdateDate = model.UpdateDate,
            Tasks = model.Tasks?.Select(TaskMapper.MapTaskDbModelToTaskModel).ToList() ?? new System.Collections.Generic.List<TaskModel>()
        };
    }
}

