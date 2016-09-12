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
                  AND name = 'Apikey')
  BEGIN
      PRINT 'Dropping Table Apikey'

      DROP TABLE Apikey
  END

go

CREATE TABLE Apikey
  (
     ApikeyId       INT NOT NULL PRIMARY KEY,
     CreatedDate    DATETIME NULL,
     ModifiedDate   DATETIME NULL,
     ExpirationDate DATETIME NULL,
     IsDisabled     BIT NULL,
     Token        NVARCHAR(255) NULL,
     Notes           NVARCHAR(255) NULL
  )

go

IF Object_id('Apikey') IS NOT NULL
  PRINT '<<< CREATED TABLE Apikey >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE Apikey >>>'

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
                  AND name = 'login_role')
  BEGIN
      PRINT 'Dropping Table login_role'

      DROP TABLE login_role
  END

go

CREATE TABLE login_role
  (
     login_role_id NUMERIC(10) NOT NULL PRIMARY KEY,
     date_created DATETIME NULL,
     code         NVARCHAR(255) NULL,
     description  NVARCHAR(255) NULL,
     visible_code NVARCHAR(255) NULL
  )

go

IF Object_id('login_role') IS NOT NULL
  PRINT '<<< CREATED TABLE login_role >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE login_role >>>'

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
                  AND name = 'login')
  BEGIN
      PRINT 'Dropping Table login'

      DROP TABLE login
  END

go

CREATE TABLE login
  (
     login_id        NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
	 login_role_id        NUMERIC(10) NULL,
     date_created      DATETIME NULL,
     date_modified     DATETIME NULL,
     loginname        NVARCHAR(255) NULL,
     password_encrypted    NVARCHAR(255) NULL,
     encryption_algorithm    NVARCHAR(255) NULL,
	 welcome_email_sent                   BIT NULL,
	 is_active                   BIT NULL,
     num_of_tries                 INT NULL
  )

go

IF Object_id('login') IS NOT NULL
  PRINT '<<< CREATED TABLE login >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE login >>>'

go
/******************************************************************************
**		Name:  Lookup Data
**		Desc: Creates the lookup or static for the project
**
**		Auth: Haytham Allos
**		Date: August 23, 2016
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		8/23/146		HA			Created
**    
*******************************************************************************/
delete from [Apikey]
INSERT INTO [Apikey] (ApikeyId, CreatedDate, Token, Notes) VALUES (1, GETDATE(), '6cfebbdd8b6a41babab5b644ab86a456', 'MAINWEB');
INSERT INTO [Apikey] (ApikeyId, CreatedDate, Token, Notes) VALUES (2, GETDATE(), '94ac2960f33040ad9cc6311a87065acb', '');
INSERT INTO [Apikey] (ApikeyId, CreatedDate, Token, Notes) VALUES (3, GETDATE(), 'd63e8eafc3c54c90a75afa581fd05e10', '');
INSERT INTO [Apikey] (ApikeyId, CreatedDate, Token, Notes) VALUES (4, GETDATE(), '7b6a27958fef4ddd99d652e432e564a7', '');
INSERT INTO [Apikey] (ApikeyId, CreatedDate, Token, Notes) VALUES (5, GETDATE(), '5d1c6e75feff40b08ed5966d28df0db9', '');
GO