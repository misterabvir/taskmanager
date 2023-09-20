-- Active: 1695048300061@@127.0.0.1@3306@TaskManagerDb

USE TaskManagerDb;
DROP TABLE IF EXISTS Tasks;
CREATE TABLE Tasks(
	TaskId CHAR(36) PRIMARY KEY UNIQUE,
    TaskName VARCHAR(255),
	Description TEXT,
	ProjectId CHAR(36),
	StartDate DateTime,
	CancelDate DateTime,
	CreateDate DateTime,
	UpdateDate DateTime
);

SELECT 
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
	            t.StartDate,
				(select p.ProjectName FROM `Projects` p WHERE t.`ProjectId` = p.`ProjectId`) as ProjectName,
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
	            Comments c ON c.TaskId = t.TaskId;

				UPDATE Projects SET UpdateDate=@CreateDate WHERE ProjectId = (SELECT ProjectId FROM Tasks WHERE TaskId=@TaskId);
