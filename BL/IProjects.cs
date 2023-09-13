using Domain;

namespace BL;

public interface IProjects
{
    Task<ProjectModel> Create(string projectName);
    Task<ProjectModel> Update(Guid projectId, string? projectName = null);
    Task<IEnumerable<ProjectModel>> GetAll();
    Task<ProjectModel> GetById(Guid id);
    Task<ProjectModel> GetByName(string projectName);
}
