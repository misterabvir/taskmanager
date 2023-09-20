namespace DAL.Base;

internal static class SQL
{
    internal static class Project
    {
        public static string SplitOnByTaskId => "TaskId";
        public static string Create =>
            @"INSERT INTO 
                Projects(
                    ProjectId, 
                    ProjectName, 
                    CreateDate, 
                    UpdateDate) 
              VALUES(
                @ProjectId, 
                @ProjectName, 
                @CreateDate, 
                @UpdateDate)";
        public static string Update =>
            @"UPDATE 
                Projects 
              SET 
                ProjectName = @ProjectName,                   
                CreateDate = @CreateDate, 
                UpdateDate = @UpdateDate
              WHERE 
                ProjectId=@ProjectId;";

        public static string GetAll =>
            @"SELECT 
	            p.ProjectId,
	            p.ProjectName,
	            p.CreateDate,
	            p.UpdateDate,
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
				t.Description,
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
				t.Description,
	            t.StartDate,
	            t.CreateDate,
	            t.UpdateDate,
	            t.CancelDate
            FROM 
	            Projects p
            LEFT JOIN 
	            Tasks t ON t.ProjectId = p.ProjectId
            WHERE p.ProjectId = @ProjectId;";
    }

    internal static class Task
    {
        public static string SplitOnByCommentId => "CommentId";
        public static string Create =>
            @"INSERT INTO 
                Tasks(
                    TaskId, 
                    TaskName, 
                    ProjectId, 
                    StartDate, 
                    CancelDate, 
                    CreateDate, 
                    UpdateDate) 
              VALUES(
                @TaskId, 
                @TaskName, 
                @ProjectId, 
                @StartDate, 
                @CancelDate, 
                @CreateDate, 
                @UpdateDate);
              UPDATE 
                Projects 
			  SET 
                UpdateDate = @UpdateDate 
              WHERE 
                ProjectId = @ProjectId;";
        public static string Update =>
            @"UPDATE Tasks 
              SET TaskName = @TaskName, 
                  Description = @Description, 
                  ProjectId = @ProjectId, 
                  StartDate = @StartDate, 
                  CancelDate = @CancelDate, 
                  UpdateDate = @UpdateDate
              WHERE TaskId = @TaskId;
			  UPDATE Projects 
			  SET UpdateDate = @UpdateDate 
              WHERE ProjectId = @ProjectId";

        public static string GetByTaskId =>
            @"SELECT 
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
	            t.StartDate,
				t.Description,
				(SELECT 
                    p.ProjectName 
                 FROM 
                    Projects p 
                 WHERE 
                    t.`ProjectId` = p.`ProjectId`) as ProjectName,
	            t.CreateDate,
	            t.UpdateDate,
	            t.CancelDate,
	            c.CommentId,
	            c.TaskId,
	            c.Content,
	            c.CreateDate
            FROM 
	            Tasks t
            LEFT JOIN 
	            Comments c ON c.TaskId = t.TaskId
            WHERE
                t.TaskId=@TaskId;";
    }

    internal static class Comments
    {
        public static string Create =>
            @"INSERT INTO 
                Comments(
                    CommentId,
                    TaskId, 
                    CreateDate, 
                    Content) 
              VALUES(
                @CommentId, 
                @TaskId, 
                @CreateDate, 
                @Content);
			  UPDATE 
                Tasks 
              SET 
                UpdateDate=@CreateDate 
              WHERE TaskId=@TaskId;
			  UPDATE 
                Projects 
              SET 
                UpdateDate=@CreateDate 
              WHERE ProjectId = 
                (SELECT 
                    ProjectId 
                 FROM 
                    Tasks 
                 WHERE 
                    TaskId=@TaskId);";
    }
}
