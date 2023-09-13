using DAL.Base;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL;

public class ProjectsDAL : IProjectsDAL
{
    private readonly IRepository repository;
    public ProjectsDAL(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(ProjectModel model)
    {
        await repository.ExecuteAsync(SQL.Project.Create, model);
    }

    public async Task<IEnumerable<ProjectModel>> GetAll()
    {
        return await repository.QueryAsync<ProjectModel>(SQL.Project.GetAll, new { });
    }

    public async Task<ProjectModel> GetById(Guid id)
    {
        return await repository.QuerySingleOrDefaultAsync<ProjectModel>(SQL.Project.GetById, new { Id = id });
    }
    public async Task Update(ProjectModel model)
    {
        await repository.ExecuteAsync(SQL.Project.Update, model);
    }
}
