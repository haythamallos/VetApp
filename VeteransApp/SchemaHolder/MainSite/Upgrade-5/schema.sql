ALTER TABLE jct_user_content_type DROP COLUMN RatingLeft, RatingRight
go

delete from side
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'NONE', 'None', 'None')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'LEFT_SIDE', 'Left side', 'Left side')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'RIGHT_SIDE', 'Right side', 'Right side')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'BOTH_SIDES', 'Both sides', 'Both sides')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (5, GETDATE(), 'BILATERAL_UPPER', 'Bilateral Upper', 'Bilateral Upper')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (6, GETDATE(), 'BILATERAL_LOWER', 'Bilateral Lower', 'Bilateral Lower')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (7, GETDATE(), 'BILATERAL_RIGHT_UPPER', 'Bilateral Right Upper', 'Bilateral Right Upper')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (8, GETDATE(), 'BILATERAL_LEFT_UPPER', 'Bilateral Left Upper', 'Bilateral Left Upper')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (9, GETDATE(), 'BILATERAL_RIGHT_LOWER', 'Bilateral Right Lower', 'Bilateral Right Lower')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (10, GETDATE(), 'BILATERAL_LEFT_LOWER', 'Bilateral Left Lower', 'Bilateral Left Lower')
go

alter table jct_user_content_type add is_connected BIT NULL
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (6, GETDATE(), 1, 5,'Upgrade-5')
GO