/******************************************************************************
**		File: DBSchema.sql
**		Desc: Schema implementation.  Creates complete schema for SQLServer db.
**
**		Auth: Haytham Allos
**		Date: August 20, 2016
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		8/20/16		HA		Created
**    
*******************************************************************************/



/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/26/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'user_role')
  BEGIN
      PRINT 'Dropping Table user_role'

      DROP TABLE user_role
  END

go

CREATE TABLE user_role
  (
     user_role_id NUMERIC(10) NOT NULL PRIMARY KEY,
     date_created    DATETIME NULL,
     code            NVARCHAR(255) NULL,
     [description]     NVARCHAR(255) NULL,
     visible_code    NVARCHAR(255) NULL
  )

go

IF Object_id('user_role') IS NOT NULL
  PRINT '<<< CREATED TABLE user_role >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE user_role >>>'
go


/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		9/7/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'user')
  BEGIN
      PRINT 'Dropping Table user'

      DROP TABLE [user]
  END

go

CREATE TABLE [user]
  (
     user_id        NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
	 user_role_id [numeric](10, 0) NULL,
     date_created      DATETIME NULL,
     date_modified     DATETIME NULL,
     fullname        NVARCHAR(255) NULL,
     firstname        NVARCHAR(255) NULL,
     middlename        NVARCHAR(255) NULL,
     lastname        NVARCHAR(255) NULL,
     phone_number        NVARCHAR(255) NULL,
	 username        NVARCHAR(255) NULL,
	 passwd        NVARCHAR(255) NULL,
	 ssn        NVARCHAR(255) NULL,
     picture_url        NVARCHAR(255) NULL,
	 picture           VARBINARY(max) NULL,
	 is_disabled                   BIT NULL,
	 welcome_email_sent                   BIT NULL,
	 validationtoken  NVARCHAR(255) NULL,
     validationlink  NVARCHAR(255) NULL,
     isvalidated  BIT NULL,
     welcome_email_sent_date  DATETIME NULL,
     last_login_date  DATETIME NULL,
	 internal_notes        text NULL,
	 user_message        text NULL,
	 cookie_id  NVARCHAR(255) NULL,
	 current_rating	int default 0,
	 security_question  NVARCHAR(255) NULL,
     security_answer  NVARCHAR(255) NULL,
	 number_of_visits	int default 0,
	 previous_visit_date     DATETIME NULL,
	 last_visit_date     DATETIME NULL
 )

go

IF Object_id('user') IS NOT NULL
  PRINT '<<< CREATED TABLE user >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE user >>>'

go

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'syslog')
  BEGIN
      PRINT 'Dropping Table syslog'

      DROP TABLE syslog
  END

go

CREATE TABLE syslog
  (
     syslog_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     interaction_id NUMERIC(10) NULL,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
     msgsource      NVARCHAR(255) NULL,
     msgaction      NVARCHAR(255) NULL,
     msgtxt         TEXT NULL
  )

go

IF Object_id('syslog') IS NOT NULL
  PRINT '<<< CREATED TABLE syslog >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE syslog >>>'

go

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'evaluation')
  BEGIN
      PRINT 'Dropping Table evaluation'

      DROP TABLE evaluation
  END

go

CREATE TABLE evaluation
  (
     evaluation_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     [user_id] NUMERIC(10) NULL,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
	 is_firsttime_filing	BIT NULL,
	 has_a_claim	BIT NULL,
	 has_active_appeal	BIT NULL,
	 current_rating	int default 0
  )

go

IF Object_id('evaluation') IS NOT NULL
  PRINT '<<< CREATED TABLE evaluation >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE evaluation >>>'

go


/******************************************************************************
**		Name:  Lookup Data
**		Desc: Creates the lookup or static for the project
**
**		Auth: Haytham Allos
**		Date: 11/16/16
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		HA			Created
**    
*******************************************************************************/

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		BH			Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'dbversion')
  BEGIN
      PRINT 'Dropping Table dbversion'

      DROP TABLE dbversion
  END

go

CREATE TABLE dbversion
  (
    [dbversion_id] NUMERIC(10) NOT NULL PRIMARY KEY,
	[date_created] [datetime] NULL,
	[major_num] [int] NULL DEFAULT ((0)),
	[minor_num] [int] NULL DEFAULT ((0)),
	[notes] [text] NULL
  )

go

IF Object_id('dbversion') IS NOT NULL
  PRINT '<<< CREATED TABLE dbversion >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE dbversion >>>'

go

delete from user_role
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'USER_ROLE_REGULAR', 'User role regular', 'User role regular')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'USER_ROLE_MANAGER', 'User role manager', 'User role manager')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'USER_ROLE_ADMIN', 'User role admin', 'User role admin')

delete from dbversion
INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (1, GETDATE(), 1, 0,'Inital database')
GO



