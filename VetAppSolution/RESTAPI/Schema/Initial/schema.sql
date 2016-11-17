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
**		8/20/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'apikey')
  BEGIN
      PRINT 'Dropping Table apikey'

      DROP TABLE apikey
  END

go

CREATE TABLE apikey
  (
     apikey_id       INT NOT NULL PRIMARY KEY,
     date_created      DATETIME NULL,
     date_expiration DATETIME NULL,
     is_disabled     BIT NULL,
     token        NVARCHAR(255) NULL,
     notes           NVARCHAR(255) NULL
  )

go

IF Object_id('apikey') IS NOT NULL
  PRINT '<<< CREATED TABLE apikey >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE apikey >>>'

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
     AuthUserid        NVARCHAR(255) NULL,
     AuthConnection        NVARCHAR(255) NULL,
     AuthProvider        NVARCHAR(255) NULL,
     AuthAccessToken        NVARCHAR(255) NULL,
     AuthIdToken        NVARCHAR(1024) NULL,
     date_created      DATETIME NULL,
     date_modified     DATETIME NULL,
     firstname        NVARCHAR(255) NULL,
     middlename        NVARCHAR(255) NULL,
     lastname        NVARCHAR(255) NULL,
     phone_number        NVARCHAR(255) NULL,
	 email_address        NVARCHAR(255) NULL,
     profileimageurl        NVARCHAR(255) NULL,
	 is_disabled                   BIT NULL,
	 can_text_msg                   BIT NULL,
     date_text_msg_approved      DATETIME NULL,
      AuthName        NVARCHAR(255) NULL,
     AuthNickname        NVARCHAR(255) NULL
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
                  AND name = 'apilog')
  BEGIN
      PRINT 'Dropping Table apilog'

      DROP TABLE apilog
  END

go

CREATE TABLE apilog
  (
  	[apilog_id] NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
	[apikey_id] [numeric](10, 0) NULL,
	[ref_num] [numeric](10, 0) NULL,
	[date_created] [datetime] NULL,
	[msgsource] [nvarchar](255) NULL,
	[trace] [nvarchar](255) NULL,
	[is_success] [bit] NULL,
	[in_progress] [bit] NULL,
	[http_status_str] [nvarchar](255) NULL,
	[http_status_num] [int] NULL,
	[msgtxt] [text] NULL,
	[reqtxt] [text] NULL,
	[resptxt] [text] NULL,
	[duration_in_ms] [int] NULL,
	[call_start_time] [datetime] NULL,
	[call_end_time] [datetime] NULL,
	[searchtext] [nvarchar](255) NULL,
	[authuserid] [nvarchar](255) NULL
  )

go

IF Object_id('apilog') IS NOT NULL
  PRINT '<<< CREATED TABLE apilog >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE apilog >>>'

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

IF Object_id('dbo.syslog') IS NOT NULL
  PRINT '<<< CREATED TABLE syslog >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE syslog >>>'

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


delete from apikey
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (1, GETDATE(), '6cfebbdd8b6a41babab5b644ab86a456', 'MAINWEB');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (2, GETDATE(), '94ac2960f33040ad9cc6311a87065acb', '');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (3, GETDATE(), 'd63e8eafc3c54c90a75afa581fd05e10', '');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (4, GETDATE(), '7b6a27958fef4ddd99d652e432e564a7', '');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (5, GETDATE(), '5d1c6e75feff40b08ed5966d28df0db9', '');
GO

delete from dbversion
INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (1, GETDATE(), 1, 0,'Inital database')
GO



