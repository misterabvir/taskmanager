namespace DAL.Base;

internal static class SQL
{
    /// <summary> queries for `Project` table </summary>
    internal static class Project
    {
        public static string Create =>
            @"INSERT INTO Project(Id, ProjectName, CreateDate, UpdateDate) 
              VALUES(@Id, @ProjectName, @CreateDate, @UpdateDate)";
        public static string Update =>
            @"UPDATE Project SET 
                ProjectName = @ProjectName,  
                CreateDate = @CreateDate, 
                UpdateDate = @UpdateDate
              WHERE Id=@Id;";
        public static string GetAll => @"SELECT * FROM Project";
        public static string GetById => @"SELECT * FROM Project WHERE Id=@Id";
        public static string GetByName => @"SELECT * FROM Project WHERE ProjectName=@ProjectName";
    }

    /// <summary> queries for `Task` table </summary>
    internal static class Task
    {
        public static string Create =>
            @"INSERT INTO Task(Id, TaskName, ProjectId, StartDate, CancelDate, CreateDate, UpdateDate) 
              VALUES(@Id, @TaskName, @ProjectId, @StartDate, @CancelDate, @CreateDate, @UpdateDate)";
        public static string Update =>
            @"UPDATE Task 
              SET TaskName = @TaskName, 
                  ProjectId = @ProjectId, 
                  StartDate = @StartDate, 
                  CancelDate = @CancelDate, 
                  UpdateDate = @UpdateDate
              WHERE Id = @Id";
        
        public static string GetAll => @"SELECT * FROM Task";
        public static string GetById => @"SELECT * FROM Task WHERE Id=@Id";
        public static string GetByProjectId => @"SELECT * FROM Task WHERE ProjectId=@ProjectId";
    }

    /// <summary> queries for `TaskComments` table </summary>
    internal static class Comments
    {
        public static string Create =>
            @"INSERT INTO TaskComments(Id,TaskId,CommentType, Content) 
              VALUES(@Id, @TaskId, @CommentType, @Content)";
        public static string Update =>
            @"UPDATE Project 
              SET TaskId = @TaskId, 
                  CommentType = @CommentType, 
                  Content = @Content 
              WHERE Id = @Id";

        public static string GetById => @"SELECT * FROM TaskComments WHERE Id = @Id";
        public static string GetByTaskId => @"SELECT * FROM TaskComments WHERE TaskId = @TaskId";
    }
}
