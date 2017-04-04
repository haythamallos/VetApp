ALTER TABLE jct_user_content_type DROP COLUMN RatingLeft, RatingRight
go

delete from side
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'NONE', 'None', 'None')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'LEFT', 'Left', 'Left')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'RIGHT', 'Right', 'Right')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'BILATERAL_UPPER', 'Bilateral Upper', 'Bilateral Upper')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (5, GETDATE(), 'BILATERAL_LOWER', 'Bilateral Lower', 'Bilateral Lower')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (6, GETDATE(), 'RIGHT_UPPER', 'Right Upper', 'Right Upper')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (7, GETDATE(), 'LEFT_UPPER', 'Left Upper', 'Left Upper')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (8, GETDATE(), 'RIGHT_LOWER', 'Right Lower', 'Right Lower')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (9, GETDATE(), 'LEFT_LOWER', 'Left Lower', 'Left Lower')
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (6, GETDATE(), 1, 5,'Upgrade-5')
GO