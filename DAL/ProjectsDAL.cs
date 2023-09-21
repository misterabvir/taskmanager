using DAL.Base;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL;
public class ProjectsDAL : IRepository<ProjectModel>
{
    private readonly IDataAccess<ProjectModel> dataAccessStrategy;
    public ProjectsDAL(IDataAccess<ProjectModel> dataAccessStrategy)
    {
        this.dataAccessStrategy = dataAccessStrategy;
    }

    public async Task AddAsync(ProjectModel entity)
    {
        await dataAccessStrategy.AddAsync(entity);
    }

    public async Task DeleteAsync(ProjectModel entity)
    {
        await dataAccessStrategy.DeleteAsync(entity);
    }

    public async Task<IEnumerable<ProjectModel>> GetAllAsync()
    {
        return await dataAccessStrategy.GetAllAsync();
    }

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        return await dataAccessStrategy.GetByIdAsync(id);
    }

    public async Task UpdateAsync(ProjectModel entity)
    {
        await dataAccessStrategy.UpdateAsync(entity);
    }
}