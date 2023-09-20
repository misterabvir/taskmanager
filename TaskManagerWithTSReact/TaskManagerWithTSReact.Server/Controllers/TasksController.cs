using BL;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWithTSReact.Server.Mappers;
using TaskManagerWithTSReact.Server.ViewModels;
using TaskManagerWithTSReact.Server.ViewModels.PostRequestModel;

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

    [HttpPost]
    [Route("/taskDetail")]
    public async Task<TaskViewModel> GetTaskDetail(TaskDetailModel model)
    {
        var task = await tasks.GetById(model.TaskId);
        return TaskMapper.MapTaskModelToTaskViewModel(task);
    }


    [HttpPost]
    [Route("/startTask")]
    public async Task<IActionResult> StartTask(ActionTaskModel model)
    {
        await tasks.Start(model.TaskId);
        return Ok();
    }

    [HttpPost]
    [Route("/cancelTask")]
    public async Task<IActionResult> CancelTask(ActionTaskModel model)
    {
        await tasks.Cancel(model.TaskId);
        return Ok();
    }

    [HttpPost]
    [Route("/saveTaskName")]
    public async Task<IActionResult> SaveTask(SaveNewTaskNameModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
            return BadRequest();
        await tasks.UpdateName(model.TaskId, model.Name);
        return Ok();
    }
    [HttpPost]
    [Route("/saveDescription")]
    public async Task<IActionResult> SaveTaskDescriprion(SaveTaskDescription model)
    {
        if (string.IsNullOrWhiteSpace(model.Description))
            return BadRequest();
        await tasks.UpdateDescription(model.TaskId, model.Description);
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


