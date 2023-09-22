
USE TaskManagerDb;
DROP TABLE IF EXISTS Tasks;
CREATE TABLE Tasks(
	TaskId CHAR(36) PRIMARY KEY,
    TaskName VARCHAR(255),
	Description TEXT,
	ProjectId CHAR(36),
	StartDate DateTime,
	CancelDate DateTime,
	CreateDate DateTime,
	UpdateDate DateTime
);

ALTER TABLE Tasks
   ADD CONSTRAINT FK_ProjectId FOREIGN KEY (ProjectId)
      REFERENCES Projects (ProjectId)
      ON DELETE CASCADE
      ON UPDATE CASCADE;