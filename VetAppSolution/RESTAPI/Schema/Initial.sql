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
                  AND name = 'member')
  BEGIN
      PRINT 'Dropping Table member'

      DROP TABLE member
  END

go

CREATE TABLE member
  (
     member_id        NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     authtoken        NVARCHAR(255) NULL,
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
delete from apikey
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (1, GETDATE(), '6cfebbdd8b6a41babab5b644ab86a456', 'MAINWEB');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (2, GETDATE(), '94ac2960f33040ad9cc6311a87065acb', '');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (3, GETDATE(), 'd63e8eafc3c54c90a75afa581fd05e10', '');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (4, GETDATE(), '7b6a27958fef4ddd99d652e432e564a7', '');
INSERT INTO apikey (apikey_id, date_created, token, notes) VALUES (5, GETDATE(), '5d1c6e75feff40b08ed5966d28df0db9', '');
GO
/*********************** CUSTOM  BEGIN *********************/
IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberExistByAuthtoken]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberExistByAuthtoken >>>>'
	DROP PROCEDURE [spMemberExistByAuthtoken]
END
GO

CREATE PROCEDURE spMemberExistByAuthtoken
(
@Authtoken                 NVARCHAR(255) = NULL,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(authtoken) 
FROM [member] 
WHERE authtoken = @Authtoken
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberExistByAuthtoken]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberExistByAuthtoken >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberExistByAuthtoken >>>'
GO
/*********************** CUSTOM  END *********************/



IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberExist >>>>'
	DROP PROCEDURE [spMemberExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/21/2016		HA		Created
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

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberLoad >>>>'
	DROP PROCEDURE [spMemberLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/21/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberLoad
(
@MemberID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [member_id], [authtoken], [date_created], [date_modified], [firstname], [middlename], [lastname], [profileimageurl], [is_disabled] 
FROM [member] 
WHERE member_id = @MemberID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberUpdate >>>>'
	DROP PROCEDURE [spMemberUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/21/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberUpdate
(
	@MemberID                  NUMERIC(10) = 0,
	@Authtoken                 NVARCHAR(255) = NULL,
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
	[authtoken] = @Authtoken,
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

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberDelete >>>>'
	DROP PROCEDURE [spMemberDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/21/2016		HA		Created
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

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberInsert >>>>'
	DROP PROCEDURE [spMemberInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/21/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spMemberInsert
(
	@MemberID                  NUMERIC(10) = 0,
	@Authtoken                 NVARCHAR(255) = NULL,
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
	[authtoken],
	[date_created],
	[firstname],
	[middlename],
	[lastname],
	[profileimageurl],
	[is_disabled]
)
 VALUES 
(
	@Authtoken,
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

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spMemberEnum >>>>'
	DROP PROCEDURE [spMemberEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spMemberEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/21/2016		HA		Created
*******************************************************************************/




CREATE PROCEDURE spMemberEnum
	@MemberID                  NUMERIC(10) = 0,
	@Authtoken                 NVARCHAR(255) = NULL,
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


      SELECT  [member_id], [authtoken], [date_created], [date_modified], [firstname], [middlename], [lastname], [profileimageurl], [is_disabled]
      FROM [member] 
      WHERE ((@MemberID = 0) OR ([member_id] LIKE @MemberID))
      AND ((@Authtoken IS NULL) OR ([authtoken] LIKE @Authtoken))
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
IF EXISTS (select * from sysobjects where id = object_id(N'[spMemberEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spMemberEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spMemberEnum >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApikeyExist >>>>'
	DROP PROCEDURE [spApikeyExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApikeyExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApikeyExist
(
@ApikeyID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(apikey_id) 
FROM [apikey] 
WHERE apikey_id = @ApikeyID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApikeyExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApikeyExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApikeyLoad >>>>'
	DROP PROCEDURE [spApikeyLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApikeyLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApikeyLoad
(
@ApikeyID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [apikey_id], [date_created], [date_expiration], [is_disabled], [token], [notes] 
FROM [apikey] 
WHERE apikey_id = @ApikeyID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApikeyLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApikeyLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApikeyUpdate >>>>'
	DROP PROCEDURE [spApikeyUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApikeyUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApikeyUpdate
(
	@ApikeyID                  NUMERIC(10) = 0,
	@DateExpiration            DATETIME = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@Token                     NVARCHAR(255) = NULL,
	@Notes                     NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [apikey] SET 
	[date_expiration] = @DateExpiration,
	[is_disabled] = @IsDisabled,
	[token] = @Token,
	[notes] = @Notes
WHERE apikey_id = @ApikeyID

-- return ID for updated record
SELECT @PKID = @ApikeyID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApikeyUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApikeyUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApikeyDelete >>>>'
	DROP PROCEDURE [spApikeyDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApikeyDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApikeyDelete
(
@ApikeyID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [apikey] 
WHERE apikey_id = @ApikeyID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApikeyDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApikeyDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApikeyInsert >>>>'
	DROP PROCEDURE [spApikeyInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApikeyInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApikeyInsert
(
	@ApikeyID                  NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@DateExpiration            DATETIME = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@Token                     NVARCHAR(255) = NULL,
	@Notes                     NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [apikey]
(
	[apikey_id],
	[date_created],
	[date_expiration],
	[is_disabled],
	[token],
	[notes]
)
 VALUES 
(
	@ApikeyID,
	@DateCreated,
	@DateExpiration,
	@IsDisabled,
	@Token,
	@Notes
)


-- return ID for new record
SELECT @PKID = @ApikeyID

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApikeyInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApikeyInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApikeyEnum >>>>'
	DROP PROCEDURE [spApikeyEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApikeyEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/20/2016		HA		Created
*******************************************************************************/




CREATE PROCEDURE spApikeyEnum
	@ApikeyID                  NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateExpiration       DATETIME = NULL,
    	@EndDateExpiration         DATETIME = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@Token                     NVARCHAR(255) = NULL,
	@Notes                     NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [apikey_id], [date_created], [date_expiration], [is_disabled], [token], [notes]
      FROM [apikey] 
      WHERE ((@ApikeyID = 0) OR ([apikey_id] LIKE @ApikeyID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateExpiration IS NULL) OR ([date_expiration] >= @BeginDateExpiration))
      AND ((@EndDateExpiration IS NULL) OR ([date_expiration] <= @EndDateExpiration))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
      AND ((@Token IS NULL) OR ([token] LIKE @Token))
      AND ((@Notes IS NULL) OR ([notes] LIKE @Notes))
 ORDER BY [apikey_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spApikeyEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApikeyEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApikeyEnum >>>'
GO


