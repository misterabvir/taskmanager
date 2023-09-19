using Domain;
using TaskManager.ViewModels;

namespace TaskManager.Mappers;

public static class ProjectsMapper
{
    public static ProjectsListViewModel MapProjectModelToProjectListViewModel(ProjectModel model)
    {
        return new ProjectsListViewModel()
        {
            Id = model.ProjectId,
            ProjectName = model.ProjectName
        };
    }

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
