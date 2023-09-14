using BL;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskManager.Mappers;
using TaskManager.ViewModels;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IProjects projects;
    private readonly ITasks tasks;
    public TasksController(IProjects projects, ITasks tasks)
    {
        this.projects = projects;
        this.tasks = tasks;
    }
    
    [HttpGet]
    [Route("/tasklist")]
    public async Task<IEnumerable<TaskViewModel>> Get()
    {
        var taskList = await tasks.GetAll();
        var projectList = await projects.GetAll();
        var taskvm = taskList.Select(task =>
            TaskMapper.MapTaskModelToTaskViewModel(
                task, 
                projectList.First(project => project.Id == task.ProjectId)!.ProjectName!));
        return taskvm;
    }

    [HttpPost]
    [Route("/startTask")]
    public async Task<IActionResult> StartTask(PostRequestActionTaskModel model)
    {
        await tasks.Start(model.Id);
        
        return Ok();
    }

    [HttpPost]
    [Route("/cancelTask")]
    public async Task<IActionResult> CancelTask(PostRequestActionTaskModel model)
    {
        await tasks.Cancel(model.Id);
        return Ok();
    }
}


