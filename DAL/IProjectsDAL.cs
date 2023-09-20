using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL;

public interface IProjectsDAL
{
    Task Create(ProjectModel model);

    Task Update(ProjectModel model);

    Task<IEnumerable<ProjectModel>> GetAll();
    
    Task<ProjectModel> GetById(Guid id);
}
