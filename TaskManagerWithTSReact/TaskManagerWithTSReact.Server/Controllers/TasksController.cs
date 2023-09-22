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
    private readonly ITasks tasks;
    public TasksController(IProjects projects, ITasks tasks)
    {
        this.tasks = tasks;
    }

    [HttpGet]
    [Route("/Task/Detail/{taskId}")]
    public async Task<TaskViewModel> GetTaskDetail(Guid taskId)
    {
        var task = await tasks.GetById(taskId);
        return TaskMapper.MapTaskModelToTaskViewModel(task);
    }


    [HttpPut]
    [Route("/Task/Start")]
    public async Task<IActionResult> StartTask(ActionTaskModel model)
    {
        await tasks.Start(model.TaskId);
        return Ok();
    }

    [HttpPut]
    [Route("/Task/Cancel")]
    public async Task<IActionResult> CancelTask(ActionTaskModel model)
    {
        await tasks.Cancel(model.TaskId);
        return Ok();
    }

    [HttpPut]
    [Route("/Task/UpdateName")]
    public async Task<IActionResult> SaveTask(SaveNewTaskNameModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
            return BadRequest();
        await tasks.UpdateName(model.TaskId, model.Name);
        return Ok();
    }
    [HttpPut]
    [Route("/Task/UpdateDescription")]
    public async Task<IActionResult> SaveTaskDescriprion(SaveTaskDescription model)
    {
        if (string.IsNullOrWhiteSpace(model.Description))
            return BadRequest();
        await tasks.UpdateDescription(model.TaskId, model.Description);
        return Ok();
    }

    [HttpPost]
    [Route("/Task/Create")]
    public async Task<IActionResult> CreateTask(CreateTaskModel model)
    {
        await tasks.Create(model.TaskName!, model.ProjectId);
        return Ok();
    }
}


