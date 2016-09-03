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