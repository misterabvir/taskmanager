-- Active: 1695287194983@@127.0.0.1@1433@TaskManagerDb
USE TaskManagerDb;
DROP TABLE IF EXISTS Projects;
CREATE TABLE Projects(
	ProjectId CHAR(36) PRIMARY KEY,
	ProjectName VARCHAR(255) UNIQUE,
	CreateDate DateTime,
	UpdateDate DateTime
);
SELECT 
	            p.ProjectId,
	            p.ProjectName,
	            p.CreateDate as ProjectCreate,
	            p.UpdateDate as ProjectUpdate,
	            t.TaskId,
	            t.ProjectId,
	            t.TaskName,
	            t.StartDate as TaskStart,
	            t.CreateDate as TaskCreate,
	            t.UpdateDate as TaskUpdate,
	            t.CancelDate as TaskCancel
            FROM 
	            Projects p
            LEFT JOIN 
	            Tasks t ON t.ProjectId = p.ProjectId;