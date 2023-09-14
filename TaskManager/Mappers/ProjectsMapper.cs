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
}
