-- Active: 1695048300061@@127.0.0.1@3306@TaskManagerDb

USE TaskManagerDb;
DROP TABLE IF EXISTS TaskComments;
CREATE TABLE TaskComments(
	Id CHAR(36) PRIMARY KEY UNIQUE,
	TaskId CHAR(36),
	CreateDate DATETIME,
	Content TEXT
);
