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

    public static ProjectViewModel MapProjectModelToProjectViewModel(ProjectModel model, IEnumerable<TaskViewModel> tasks)
    {
        return new ProjectViewModel()
        {
            Id = model.ProjectId,
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
            Tasks = tasks            
        };
    }
}
