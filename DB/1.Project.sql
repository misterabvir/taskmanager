-- Active: 1694541457173@@127.0.0.1@1433
USE TaskManagerDb;
DROP TABLE IF EXISTS Project;
CREATE TABLE Project(
	Id CHAR(16) NOT NULL UNIQUE,
	ProjectName VARCHAR(255),
	CreateDate DateTime,
	UpdateDate DateTime
);
