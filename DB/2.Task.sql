
USE TaskManagerDb;
DROP TABLE IF EXISTS Task;
CREATE TABLE Task(
	Id CHAR(16) NOT NULL UNIQUE,
    TaskName VARCHAR(255),
	ProjectId CHAR(16),
	StartDate DateTime,
	CancelDate DateTime,
	CreateDate DateTime,
	UpdateDate DateTime,
    CONSTRAINT fk_project_task FOREIGN KEY (ProjectId) REFERENCES Project(Id) 
);
