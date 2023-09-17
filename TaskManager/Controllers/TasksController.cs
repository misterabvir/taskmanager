using BL;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskManager.Mappers;
using TaskManager.ViewModels;
using TaskManager.ViewModels.PostRequestModel;

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
    [Route("/taskList")]
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
    [Route("/getTask")]
    public async Task<TaskViewModel> GetTask(TaskDetailModel model)
    {
        var task = await tasks.GetById(model.Id);
        var project = await projects.GetById(task.ProjectId);
        return TaskMapper.MapTaskModelToTaskViewModel(task, project.ProjectName!);
    }


    [HttpPost]
    [Route("/startTask")]
    public async Task<IActionResult> StartTask(ActionTaskModel model)
    {
        await tasks.Start(model.Id);
        
        return Ok();
    }

    [HttpPost]
    [Route("/cancelTask")]
    public async Task<IActionResult> CancelTask(ActionTaskModel model)
    {
        await tasks.Cancel(model.Id);
        return Ok();
    }

    [HttpPost]
    [Route("/saveTask")]
    public async Task<IActionResult> SaveTask(SaveNewTaskNameModel model)
    {
        if(string.IsNullOrWhiteSpace(model.Name))
            return BadRequest();
        await tasks.UpdateName(model.Id, model.Name);
        return Ok();
    }


    [HttpPost]
    [Route("/createTask")]
    public async Task<IActionResult> CreateTask(CreateTaskModel model)
    {
        await tasks.Create(model.TaskName!, model.ProjectId);
        return Ok();
    }
}


