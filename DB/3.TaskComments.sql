
USE TaskManagerDb;
DROP TABLE IF EXISTS TaskComments;
CREATE TABLE TaskComments(
	Id CHAR(16) NOT NULL UNIQUE,
	TaskId CHAR(16),
	CommentType int,
	Content TEXT,
    CONSTRAINT fk_task_comment FOREIGN KEY (TaskId) REFERENCES Task(Id) 
);