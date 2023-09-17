using Domain;
using TaskManager.ViewModels;

namespace TaskManager.Mappers;

public static class ProjectsMapper
{
    public static ProjectsListViewModel MapProjectModelToProjectListViewModel(ProjectModel model)
    {
        return new ProjectsListViewModel()
        {
            Id = model.Id,
            ProjectName = model.ProjectName
        };
    }

    public static ProjectsViewModel MapProjectModelToProjectViewModel(ProjectModel model, IEnumerable<TaskViewModel> tasks)
    {
        return new ProjectsViewModel()
        {
            Id = model.Id,
            ProjectName = model.ProjectName,
            CreateDate = model.CreateDate,
            UpdateDate = model.UpdateDate,
            Tasks = tasks            
        };
    }
}
