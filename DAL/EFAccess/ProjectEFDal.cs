using DAL.Base;
using DAL.EFAccess.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EFAccess;

public class ProjectEFDal : IDataAccess<ProjectModel>
{
    private readonly TaskManagerDbContext context;
    public ProjectEFDal(TaskManagerDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(ProjectModel entity)
    {
        DbModels.Project model = ProjectMapper.MapProjectModelToProjectDbModel(entity);
        await context.Projects.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public Task DeleteAsync(ProjectModel entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProjectModel>> GetAllAsync()
    {
        var dbModels = await context.Projects.ToListAsync();
        foreach (var model in dbModels)
        {
            await context.Entry(model).Collection(m => m.Tasks).LoadAsync();
        }
        return dbModels.Select(ProjectMapper.MapProjectDbModelToProjectModel);
    }

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        DbModels.Project model = context.Projects.First(p => p.ProjectId == id.ToString());
        await context.Entry(model).Collection(m=>m.Tasks).LoadAsync();
        return ProjectMapper.MapProjectDbModelToProjectModel(model);
    }

    public async Task UpdateAsync(ProjectModel entity)
    {
        DbModels.Project model = context.Projects.First(p => p.ProjectId == entity.ProjectId.ToString());
        model.UpdateDate = entity.UpdateDate;
        model.ProjectName = entity.ProjectName;
        await context.SaveChangesAsync();
    }
}
