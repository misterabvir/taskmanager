using DAL;
using DAL.Base;
using Domain;

namespace BL;

public class Projects : IProjects
{
    private readonly IRepository<ProjectModel> projectDAL;
    public Projects(IRepository<ProjectModel> projectDAL)
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
        await projectDAL.AddAsync(model);
        return model;
    }
    public async Task<ProjectModel> Update(Guid projectId, string? projectName = null)
    {
        ProjectModel model = await projectDAL.GetByIdAsync(projectId);
        if (model == null) throw new Exception("project has been not exixts");
        if (projectName != null) model.ProjectName = projectName;
        model.UpdateDate = DateTime.Now;
        await projectDAL.UpdateAsync(model);
        return model;
    }
    public async Task<IEnumerable<ProjectModel>> GetAll()
    {
        return await projectDAL.GetAllAsync();
    }

    public async Task<ProjectModel> GetById(Guid id)
    {
        return await projectDAL.GetByIdAsync(id);
    }
}
