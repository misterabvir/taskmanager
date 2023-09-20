using Domain;
using TaskManagerWithTSReact.Server.ViewModels;

namespace TaskManagerWithTSReact.Server.Mappers;

public static class ProjectsMapper
{
    public static ProjectViewModel MapProjectModelToProjectViewModel(ProjectModel model)
    {
        return new ProjectViewModel()
        {
            ProjectId = model.ProjectId,
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
            Tasks = model.Tasks.Select(t =>
            {
                var taskVM = TaskMapper.MapTaskModelToTaskViewModel(t);
                taskVM.ProjectName = model.ProjectName!;
                return taskVM;
            }).ToList()       
        };
    }
}
