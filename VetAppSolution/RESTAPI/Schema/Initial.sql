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
     date_created    DATETIME NULL,
     date_modified   DATETIME NULL,
     date_expiration DATETIME NULL,
     is_disabled     BIT NULL,
     apiauth_token        NVARCHAR(255) NULL,
     notes           NVARCHAR(255) NULL
  )

go

IF Object_id('apikey') IS NOT NULL
  PRINT '<<< CREATED TABLE apikey >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE apikey >>>'

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
delete from [apikey]
INSERT INTO [apikey] (apikey_id, date_created, apiauth_token, notes) VALUES (1, GETDATE(), '6cfebbdd8b6a41babab5b644ab86a456', 'MAINWEB');
INSERT INTO [apikey] (apikey_id, date_created, apiauth_token, notes) VALUES (2, GETDATE(), '94ac2960f33040ad9cc6311a87065acb', '');
INSERT INTO [apikey] (apikey_id, date_created, apiauth_token, notes) VALUES (3, GETDATE(), 'd63e8eafc3c54c90a75afa581fd05e10', '');
INSERT INTO [apikey] (apikey_id, date_created, apiauth_token, notes) VALUES (4, GETDATE(), '7b6a27958fef4ddd99d652e432e564a7', '');
INSERT INTO [apikey] (apikey_id, date_created, apiauth_token, notes) VALUES (5, GETDATE(), '5d1c6e75feff40b08ed5966d28df0db9', '');
GO