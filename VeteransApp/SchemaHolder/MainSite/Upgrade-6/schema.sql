delete from user_role
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'USER_ROLE_CLIENT', 'User role client user', 'User role client user')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'USER_ROLE_STAFF', 'User role staff user', 'User role staff user')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'USER_ROLE_POWER_USER', 'User role power user', 'User role power user')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'USER_ROLE_ADMIN', 'User role admin user', 'User role admin user')

alter table [user] add user_source_id [numeric](10, 0) NULL
go

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		5/9/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'jct_user_user')
  BEGIN
      PRINT 'Dropping Table jct_user_user'

      DROP TABLE jct_user_user
  END

go

CREATE TABLE jct_user_user
  (
     jct_user_user_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
	 [user_source_id] [numeric](10, 0) NULL,
	 [user_member_id] [numeric](10, 0) NULL
  )

go

IF Object_id('jct_user_user') IS NOT NULL
  PRINT '<<< CREATED TABLE jct_user_user >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE jct_user_user >>>'

go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (7, GETDATE(), 1, 6,'Upgrade-6')
GO