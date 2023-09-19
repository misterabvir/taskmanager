namespace DAL.Base;

internal static class SQL
{
    /// <summary> queries for `Project` table </summary>
    internal static class Project
    {
        public static string SplitOnByTaskId => "TaskId";
        public static string Create =>
            @"INSERT INTO Projects(ProjectId, ProjectName, CreateDate, UpdateDate) 
              VALUES(@ProjectId, @ProjectName, @CreateDate, @UpdateDate)";
        public static string Update =>
            @"UPDATE Projects SET 
                ProjectName = @ProjectName,  
                CreateDate = @CreateDate, 
                UpdateDate = @UpdateDate
              WHERE ProjectId=@ProjectId;";

        public static string Get =>
            @"SELECT 
	            p.ProjectId,
	            p.ProjectName,
	            p.CreateDate,
	            p.UpdateDate,
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
	            t.StartDate,
	            t.CreateDate,
	            t.UpdateDate,
	            t.CancelDate
            FROM 
	            Projects p
            LEFT JOIN 
	            Tasks t ON t.ProjectId = p.ProjectId;";
            
        
        public static string GetProjectByProjectId =>
            @"SELECT 
	            p.ProjectId,
	            p.ProjectName,
	            p.CreateDate,
	            p.UpdateDate,
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
	            t.StartDate,
	            t.CreateDate,
	            t.UpdateDate,
	            t.CancelDate
            FROM 
	            Projects p
            LEFT JOIN 
	            Tasks t ON t.ProjectId = p.ProjectId
            WHERE p.ProjectId = @ProjectId;";

        public static string GetProjectByProjectName =>
            @"SELECT 
	            p.ProjectId,
	            p.ProjectName,
	            p.CreateDate,
	            p.UpdateDate,
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
	            t.StartDate,
	            t.CreateDate,
	            t.UpdateDate,
	            t.CancelDate
            FROM 
	            Projects p
            LEFT JOIN 
	            Tasks t ON t.ProjectId = p.ProjectId
            WHERE p.ProjectName = @ProjectName;";
    }

    /// <summary> queries for `Task` table </summary>
    internal static class Task
    {
        public static string Create =>
            @"INSERT INTO Tasks(TaskId, TaskName, ProjectId, StartDate, CancelDate, CreateDate, UpdateDate) 
              VALUES(@TaskId, @TaskName, @ProjectId, @StartDate, @CancelDate, @CreateDate, @UpdateDate)";
        public static string Update =>
            @"UPDATE Tasks 
              SET TaskName = @TaskName, 
                  ProjectId = @ProjectId, 
                  StartDate = @StartDate, 
                  CancelDate = @CancelDate, 
                  UpdateDate = @UpdateDate
              WHERE TaskId = @TaskId";
        
        public static string GetAll => @"SELECT * FROM Tasks";
        public static string GetById => @"SELECT * FROM Tasks WHERE TaskId=@TaskId";
        public static string GetByProjectId => @"SELECT * FROM Tasks WHERE ProjectId=@ProjectId";
    }

    /// <summary> queries for `TaskComments` table </summary>
    internal static class Comments
    {
        public static string Create =>
            @"INSERT INTO Comments(CommentId,TaskId, CreateDate, Content) 
              VALUES(@CommentId, @TaskId, @CreateDate, @Content)";
        public static string GetByTaskId => @"SELECT * FROM Comments WHERE TaskId = @TaskId";
    }
}
