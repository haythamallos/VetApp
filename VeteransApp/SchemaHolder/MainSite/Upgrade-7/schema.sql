alter table [user] add impersonate BIT NULL
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (8, GETDATE(), 1, 7,'Upgrade-7')
GO