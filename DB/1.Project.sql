
USE TaskManagerDb;
DROP TABLE IF EXISTS Projects;
CREATE TABLE Projects(
	ProjectId CHAR(36) PRIMARY KEY,
	ProjectName VARCHAR(255) UNIQUE,
	CreateDate DateTime,
	UpdateDate DateTime
);
