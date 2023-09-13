using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL;

public interface IProjectsDAL
{
    /// <summary> create new row in `Project` table </summary>
    Task Create(ProjectModel model);

    /// <summary> update row in `Project` table </summary>
    Task Update(ProjectModel model);

    /// <summary> get all rows from `Project` table </summary>
    Task<IEnumerable<ProjectModel>> GetAll();
    
    /// <summary> get row by id from `Project` table </summary>
    Task<ProjectModel> GetById(Guid id);
}
