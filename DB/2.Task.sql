-- Active: 1692086361148@@127.0.0.1@3306@TaskManagerDb

USE TaskManagerDb;
DROP TABLE IF EXISTS Task;
CREATE TABLE Task(
	Id CHAR(36) PRIMARY KEY UNIQUE,
    TaskName VARCHAR(255),
	ProjectId CHAR(36),
	StartDate DateTime,
	CancelDate DateTime,
	CreateDate DateTime,
	UpdateDate DateTime
);
