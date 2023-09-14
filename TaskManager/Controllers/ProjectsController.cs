using BL;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;
using TaskManager.Mappers;


namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjects projects;
        public ProjectsController(IProjects projects)
        {
            this.projects = projects;
        }


        [HttpGet]
        [Route("/projectslist")]
        public async Task<IEnumerable<ProjectsListViewModel>> GetProjectsList()
        {
            var projectList = await projects.GetAll();
           
            return projectList.Select(ProjectsMapper.MapProjectModelToProjectListViewModel);
        }


        [HttpPost]
        [Route("/createProject")]
        public async Task<IActionResult> CreateNewProject(PostRequestCreateProjectModel model)
        {
            if(!string.IsNullOrEmpty(model.ProjectName))
                await projects.Create(model.ProjectName!);
            return Ok();
        }
    }
}

public record RequestP(string name);