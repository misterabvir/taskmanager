-- Active: 1695048300061@@127.0.0.1@3306@TaskManagerDb
USE TaskManagerDb;
DROP TABLE IF EXISTS Projects;
CREATE TABLE Projects(
	ProjectId CHAR(36) PRIMARY KEY UNIQUE,
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