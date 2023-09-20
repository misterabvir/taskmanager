namespace TaskManagerWithTSReact.Server.ViewModels.PostRequestModel;

public record TaskDetailModel(Guid TaskId);
public record SaveTaskDescription(Guid TaskId, string Description);
public record SaveNewTaskNameModel(Guid TaskId, string Name);
public record SaveNewProjectNameModel(Guid ProjectId, string Name);
public record ProjectDetailModel(Guid ProjectId);
public record GetCommentModel(Guid TaskId);
public record CreateTaskModel(Guid ProjectId, string TaskName);
public record CreateProjectModel(string ProjectName);
public record CreateCommentModel(Guid TaskId, string Content);
public record ActionTaskModel(Guid TaskId);