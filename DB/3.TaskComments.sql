-- Active: 1692086361148@@127.0.0.1@3306@TaskManagerDb

USE TaskManagerDb;
DROP TABLE IF EXISTS TaskComments;
CREATE TABLE TaskComments(
	Id CHAR(36) PRIMARY KEY UNIQUE,
	TaskId CHAR(36),
	CommentType int,
	Content TEXT
);

UPDATE Project SET 
                ProjectName = @ProjectName,  
                CreateDate = @CreateDate, 
                UpdateDate = @UpdateDate
              WHERE Id=@Id;