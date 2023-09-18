-- Active: 1695048300061@@127.0.0.1@3306@TaskManagerDb

USE TaskManagerDb;
DROP TABLE IF EXISTS Task;
CREATE TABLE Task(
	Id CHAR(36) PRIMARY KEY UNIQUE,
    TaskName TEXT,
	ProjectId CHAR(36),
	StartDate DateTime,
	CancelDate DateTime,
	CreateDate DateTime,
	UpdateDate DateTime
);

DELETE FROM Task;
