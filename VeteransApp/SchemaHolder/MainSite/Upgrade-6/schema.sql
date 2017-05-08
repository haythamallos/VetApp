delete from user_role
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'USER_ROLE_CLIENT', 'User role client', 'User role client')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'USER_ROLE_AFFILIATE', 'User role affilate', 'User role affilate')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'USER_ROLE_STAFF', 'User role staff', 'User role staff')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'USER_ROLE_ADMIN', 'User role admin', 'User role admin')

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (7, GETDATE(), 1, 6,'Upgrade-6')
GO