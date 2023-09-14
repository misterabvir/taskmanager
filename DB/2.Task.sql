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

INSERT INTO Task(Id, TaskName, ProjectId, CreateDate, StartDate, UpdateDate, CancelDate)
VALUES
('9579e3ea-820d-4fa6-9426-5eee5f695448', 'taskname1', '09a8062b-6f6e-4095-907f-cd89dacd387a', NOW(), NULL, NOW(), NULL),
('b04ce7e9-a330-450a-9a3f-93a9beadb467', 'taskname2', '09a8062b-6f6e-4095-907f-cd89dacd387a', NOW(), NOW(), NOW(), NULL),
('cdc390fa-73b2-427c-b640-cb82fa6703c0', 'taskname1', '0b9bb4ab-b0ac-44eb-b0a3-629cecc493ac', NOW(), NOW(), NOW(), NULL),
('f8a3edee-3b57-406b-80da-c4fa3341b0f0', 'taskname2', '0b9bb4ab-b0ac-44eb-b0a3-629cecc493ac', NOW(), NOW(), NOW(), NOW()),
('3e21c0e7-e045-4ff8-958a-2329a1d873fc', 'taskname3', '0b9bb4ab-b0ac-44eb-b0a3-629cecc493ac', NOW(), NULL, NOW(), NULL),
('de733da3-9709-45de-a37f-4b9b853ab5db', 'taskname1', '40043906-8a12-4a11-aad6-53c4aa8fa3be', NOW(), NOW(), NOW(), NULL),
('898a1352-a0e2-493e-be0a-03e54a7a3d14', 'taskname1', '758fc1ff-743e-4963-879e-eb890b61c246', NOW(), NOW(), NOW(), NULL);

SELECT * FROM Task;

DELETE FROM Task;