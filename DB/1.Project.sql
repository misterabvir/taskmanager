-- Active: 1695048300061@@127.0.0.1@3306@TaskManagerDb
USE TaskManagerDb;
DROP TABLE IF EXISTS Project;
CREATE TABLE Project(
	Id CHAR(36) PRIMARY KEY UNIQUE,
	ProjectName VARCHAR(255) UNIQUE,
	CreateDate DateTime,
	UpdateDate DateTime
);
