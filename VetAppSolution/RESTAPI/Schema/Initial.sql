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
                  AND name = 'member')
  BEGIN
      PRINT 'Dropping Table member'

      DROP TABLE member
  END

go

CREATE TABLE member
  (
     member_id        NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     date_created      DATETIME NULL,
     date_modified     DATETIME NULL,
     firstname        NVARCHAR(255) NULL,
     middlename        NVARCHAR(255) NULL,
     lastname        NVARCHAR(255) NULL,
     profileimageurl        NVARCHAR(255) NULL,
	 is_disabled                   BIT NULL
  )

go

IF Object_id('member') IS NOT NULL
  PRINT '<<< CREATED TABLE member >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE member >>>'

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




IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberExist >>>>'
	DROP PROCEDURE [dbo].[spMemberExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberExist
(
@MemberID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(member_id) 
FROM [member] 
WHERE member_id = @MemberID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure dbo.spMemberExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure dbo.spMemberExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberLoad >>>>'
	DROP PROCEDURE [dbo].[spMemberLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberLoad
(
@MemberID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [member_id], [date_created], [date_modified], [firstname], [middlename], [lastname], [profileimageurl], [is_disabled] 
FROM [member] 
WHERE member_id = @MemberID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure dbo.spMemberLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure dbo.spMemberLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberUpdate >>>>'
	DROP PROCEDURE [dbo].[spMemberUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberUpdate
(
	@MemberID                  NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [member] SET 
	[date_modified] = @DateModified,
	[firstname] = @Firstname,
	[middlename] = @Middlename,
	[lastname] = @Lastname,
	[profileimageurl] = @Profileimageurl,
	[is_disabled] = @IsDisabled
WHERE member_id = @MemberID

-- return ID for updated record
SELECT @PKID = @MemberID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure dbo.spMemberUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure dbo.spMemberUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberDelete >>>>'
	DROP PROCEDURE [dbo].[spMemberDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberDelete
(
@MemberID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [member] 
WHERE member_id = @MemberID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure dbo.spMemberDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure dbo.spMemberDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberInsert >>>>'
	DROP PROCEDURE [dbo].[spMemberInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberInsert
(
	@MemberID                  NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [member]
(
	[date_created],
	[firstname],
	[middlename],
	[lastname],
	[profileimageurl],
	[is_disabled]
)
 VALUES 
(
	@DateCreated,
	@Firstname,
	@Middlename,
	@Lastname,
	@Profileimageurl,
	@IsDisabled
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure dbo.spMemberInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure dbo.spMemberInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberEnum >>>>'
	DROP PROCEDURE [dbo].[spMemberEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/




CREATE PROCEDURE spMemberEnum
	@MemberID                  NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [member_id], [date_created], [date_modified], [firstname], [middlename], [lastname], [profileimageurl], [is_disabled]
      FROM [member] 
      WHERE ((@MemberID = 0) OR ([member_id] LIKE @MemberID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@Firstname IS NULL) OR ([firstname] LIKE @Firstname))
      AND ((@Middlename IS NULL) OR ([middlename] LIKE @Middlename))
      AND ((@Lastname IS NULL) OR ([lastname] LIKE @Lastname))
      AND ((@Profileimageurl IS NULL) OR ([profileimageurl] LIKE @Profileimageurl))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
 ORDER BY [member_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[dbo].[spMemberEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure dbo.spMemberEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure dbo.spMemberEnum >>>'
GO

