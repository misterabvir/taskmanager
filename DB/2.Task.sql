-- Active: 1695048300061@@127.0.0.1@3306@TaskManagerDb

USE TaskManagerDb;
DROP TABLE IF EXISTS Tasks;
CREATE TABLE Tasks(
	TaskId CHAR(36) PRIMARY KEY UNIQUE,
    TaskName TEXT,
	ProjectId CHAR(36),
	StartDate DateTime,
	CancelDate DateTime,
	CreateDate DateTime,
	UpdateDate DateTime
);

