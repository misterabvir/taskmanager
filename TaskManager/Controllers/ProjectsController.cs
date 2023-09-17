using BL;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;
using TaskManager.Mappers;
using TaskManager.ViewModels.PostRequestModel;

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
        [Route("/projectslist")]
        public async Task<IEnumerable<ProjectsListViewModel>> GetProjectsList()
        {
            var projectList = await projects.GetAll();
           
            return projectList.Select(ProjectsMapper.MapProjectModelToProjectListViewModel);
        }


        [HttpPost]
        [Route("/createProject")]
        public async Task<IActionResult> CreateNewProject(CreateProjectModel model)
        {
            if(!string.IsNullOrEmpty(model.ProjectName))
                await projects.Create(model.ProjectName!);
            return Ok();
        }

        [HttpPost]
        [Route("/projectDetail")]
        public async Task<ProjectsViewModel> GetProjectDetail(ProjectDetailModel model)
        {
            var project = await projects.GetById(model.id);
            var projectasks = await tasks.GetByProjectId(model.id);
            return ProjectsMapper.MapProjectModelToProjectViewModel(project, projectasks.Select(task=> TaskMapper.MapTaskModelToTaskViewModel(task, project.ProjectName!)));
        }

        [HttpPost]
        [Route("/saveProject")]
        public async Task<IActionResult> SaveProject(SaveNewProjectNameModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest();
            await projects.Update(model.Id, model.Name);
            return Ok();
        }
    }
}

