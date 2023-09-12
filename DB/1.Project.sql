-- Active: 1692086361148@@127.0.0.1@3306
USE TaskManagerDb;
DROP TABLE IF EXISTS Project;
CREATE TABLE Project(
	Id CHAR(36) PRIMARY KEY UNIQUE,
	ProjectName VARCHAR(255),
	CreateDate DateTime,
	UpdateDate DateTime
);
