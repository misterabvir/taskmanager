using BL;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWithTSReact.Server.ViewModels;
using TaskManagerWithTSReact.Server.Mappers;
using TaskManagerWithTSReact.Server.ViewModels.PostRequestModel;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjects projects;
        private readonly ITasks tasks;
        public ProjectsController(IProjects projects, ITasks tasks)
        {
            this.projects = projects;
            this.tasks = tasks;
        }


        [HttpGet]
        [Route("/Project/GetAll")]
        public async Task<IEnumerable<ProjectViewModel>> GetProjectsList()
        {
            var projectList = await projects.GetAll();
            return projectList.Select(ProjectsMapper.MapProjectModelToProjectViewModel);
        }


        [HttpPost]
        [Route("/Project/Create")]
        public async Task<IActionResult> CreateNewProject(CreateProjectModel model)
        {
            if (string.IsNullOrEmpty(model.ProjectName))
                return BadRequest(new { ErrorMessage = "Project name has not been empty"});
            await projects.Create(model.ProjectName!);
            return Ok();
        }

        [HttpGet]
        [Route("/Project/Detail/{projectId}")]
        public async Task<ProjectViewModel> GetProjectDetail(Guid projectId)
        {
            var project = await projects.GetById(projectId);
            return ProjectsMapper.MapProjectModelToProjectViewModel(project);
        }

        [HttpPut]
        [Route("/Project/UpdateName")]
        public async Task<IActionResult> SaveProject(SaveNewProjectNameModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest();
            await projects.Update(model.ProjectId, model.Name);
            return Ok();
        }
    }
}

