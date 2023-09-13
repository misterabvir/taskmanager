using DAL;
using Domain;

namespace BL;

public class Projects: IProjects
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
            Id = Guid.NewGuid(),
            ProjectName = projectName,
            CreateDate = DateTime.UtcNow,
        };
        await projectDAL.Create(model);
        return model;
    }
    public async Task<ProjectModel> Update(Guid projectId, string projectName = null)
    {
        ProjectModel model = await projectDAL.GetById(projectId);
        if (model == null) throw new Exception("project has been not exixts");
        if(projectName!=null) model.ProjectName = projectName;
        model.UpdateDate = DateTime.UtcNow;
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
}
