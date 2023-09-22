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

    public Task DeleteAsync(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProjectModel>> GetAllAsync()
    {
        var projects = await context.Projects.ToListAsync();
        foreach (var project in projects)
        {
            await context
             .Entry(project)
             .Collection(p => p.Tasks)
             .LoadAsync();
        }
        return projects.Select(ProjectMapper.MapProjectDbModelToProjectModel);
    }

    public async Task<ProjectModel> GetByIdAsync(Guid id)
    {
        DbModels.Project project = await context.Projects.FirstAsync(p => p.ProjectId == id);
        await context
            .Entry(project)
            .Collection(p => p.Tasks)
            .LoadAsync();
        return ProjectMapper.MapProjectDbModelToProjectModel(project);
    }

    public async Task UpdateAsync(ProjectModel model)
    {
        DbModels.Project project = context.Projects.First(p => p.ProjectId == model.ProjectId);
        project.UpdateDate = model.UpdateDate;
        project.ProjectName = model.ProjectName;
        await context.SaveChangesAsync();
    }
}
