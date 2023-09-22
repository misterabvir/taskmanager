namespace TaskManagerWithTSReact.Server.ViewModels.PostRequestModel;

public record SaveTaskDescription(Guid TaskId, string Description);
public record SaveNewTaskNameModel(Guid TaskId, string Name);
public record SaveNewProjectNameModel(Guid ProjectId, string Name);
public record CreateTaskModel(Guid ProjectId, string TaskName);
public record CreateProjectModel(string ProjectName);
public record CreateCommentModel(Guid TaskId, string Content);
public record ActionTaskModel(Guid TaskId);
public record SaveCommentModel(Guid CommentId, string Content);
public record DeleteCommentModel(Guid CommentId);