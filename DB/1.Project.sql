-- Active: 1692086361148@@127.0.0.1@3306
USE TaskManagerDb;
DROP TABLE IF EXISTS Project;
CREATE TABLE Project(
	Id CHAR(36) PRIMARY KEY UNIQUE,
	ProjectName VARCHAR(255) UNIQUE,
	CreateDate DateTime,
	UpdateDate DateTime
);

INSERT INTO Project(Id, ProjectName, CreateDate, UpdateDate)
VALUES('09a8062b-6f6e-4095-907f-cd89dacd387a', 'name1', NOW(), NOW()),
('0b9bb4ab-b0ac-44eb-b0a3-629cecc493ac', 'name2', NOW(), NOW()),
('40043906-8a12-4a11-aad6-53c4aa8fa3be', 'name3', NOW(), NOW()),
('758fc1ff-743e-4963-879e-eb890b61c246', 'name4', NOW(), NOW());


TRUNCATE Project;