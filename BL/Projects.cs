using DAL;
using Domain;

namespace BL;

public class Projects : IProjects
{
    private readonly IProjectsDAL projectDAL;
    public Projects(IProjectsDAL projectDAL)
    {
        this.projectDAL = projectDAL;
    }

    public async Task<ProjectModel> Create(string projectName)
    {
        ProjectModel model = new()
        {
            ProjectId = Guid.NewGuid(),
            ProjectName = projectName,
            CreateDate = DateTime.Now,
        };
        await projectDAL.Create(model);
        return model;
    }
    public async Task<ProjectModel> Update(Guid projectId, string? projectName = null)
    {
        ProjectModel model = await projectDAL.GetById(projectId);
        if (model == null) throw new Exception("project has been not exixts");
        if (projectName != null) model.ProjectName = projectName;
        model.UpdateDate = DateTime.Now;
        await projectDAL.Update(model);
        return model;
    }
    public async Task<IEnumerable<ProjectModel>> GetAll()
    {
        return await projectDAL.GetAll();
    }

    public async Task<ProjectModel> GetById(Guid id)
    {
        return await projectDAL.GetById(id);
    }

    public async Task<ProjectModel> GetByName(string projectName)
    {
        return await projectDAL.GetByName(projectName);
    }
}
