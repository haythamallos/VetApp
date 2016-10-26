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
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnumByAuthUserid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserEnumByAuthUserid >>>>'
	DROP PROCEDURE [spUserEnumByAuthUserid]
END
GO

CREATE PROCEDURE spUserEnumByAuthUserid
	@UserID                    NUMERIC(10) = 0,
	@AuthUserid                NVARCHAR(255) = NULL,
	@AuthConnection            NVARCHAR(255) = NULL,
	@AuthProvider              NVARCHAR(255) = NULL,
	@AuthAccessToken           NVARCHAR(255) = NULL,
	@AuthIdToken               NVARCHAR(1024) = NULL,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@EmailAddress              NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@CanTextMsg                NUMERIC(1,0) = NULL,
    	@BeginDateTextMsgApproved  DATETIME = NULL,
    	@EndDateTextMsgApproved    DATETIME = NULL,
	@AuthName                  NVARCHAR(255) = NULL,
	@AuthNickname              NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [AuthUserid], [AuthConnection], [AuthProvider], [AuthAccessToken], [AuthIdToken], [date_created], [date_modified], [firstname], [middlename], [lastname], [phone_number], [email_address], [profileimageurl], [is_disabled], [can_text_msg], [date_text_msg_approved], [AuthName], [AuthNickname]
      FROM [user] 
      WHERE [AuthUserid]=@AuthUserid
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnumByAuthUserid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnumByAuthUserid >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnumByAuthUserid >>>'
GO
/*********************** CUSTOM  END *********************/


IF EXISTS (select * from sysobjects where id = object_id(N'[spUserExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserExist >>>>'
	DROP PROCEDURE [spUserExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/23/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserExist
(
@UserID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(user_id) 
FROM [user] 
WHERE user_id = @UserID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserLoad >>>>'
	DROP PROCEDURE [spUserLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/23/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserLoad
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_id], [AuthUserid], [AuthConnection], [AuthProvider], [AuthAccessToken], [AuthIdToken], [date_created], [date_modified], [firstname], [middlename], [lastname], [phone_number], [email_address], [profileimageurl], [is_disabled], [can_text_msg], [date_text_msg_approved], [AuthName], [AuthNickname] 
FROM [user] 
WHERE user_id = @UserID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserUpdate >>>>'
	DROP PROCEDURE [spUserUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/23/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserUpdate
(
	@UserID                    NUMERIC(10) = 0,
	@AuthUserid                NVARCHAR(255) = NULL,
	@AuthConnection            NVARCHAR(255) = NULL,
	@AuthProvider              NVARCHAR(255) = NULL,
	@AuthAccessToken           NVARCHAR(255) = NULL,
	@AuthIdToken               NVARCHAR(1024) = NULL,
	@DateModified              DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@EmailAddress              NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@CanTextMsg                NUMERIC(1,0) = 0,
	@DateTextMsgApproved       DATETIME = NULL,
	@AuthName                  NVARCHAR(255) = NULL,
	@AuthNickname              NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [user] SET 
	[AuthUserid] = @AuthUserid,
	[AuthConnection] = @AuthConnection,
	[AuthProvider] = @AuthProvider,
	[AuthAccessToken] = @AuthAccessToken,
	[AuthIdToken] = @AuthIdToken,
	[date_modified] = @DateModified,
	[firstname] = @Firstname,
	[middlename] = @Middlename,
	[lastname] = @Lastname,
	[phone_number] = @PhoneNumber,
	[email_address] = @EmailAddress,
	[profileimageurl] = @Profileimageurl,
	[is_disabled] = @IsDisabled,
	[can_text_msg] = @CanTextMsg,
	[date_text_msg_approved] = @DateTextMsgApproved,
	[AuthName] = @AuthName,
	[AuthNickname] = @AuthNickname
WHERE user_id = @UserID

-- return ID for updated record
SELECT @PKID = @UserID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserDelete >>>>'
	DROP PROCEDURE [spUserDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/23/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserDelete
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [user] 
WHERE user_id = @UserID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserInsert >>>>'
	DROP PROCEDURE [spUserInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/23/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserInsert
(
	@UserID                    NUMERIC(10) = 0,
	@AuthUserid                NVARCHAR(255) = NULL,
	@AuthConnection            NVARCHAR(255) = NULL,
	@AuthProvider              NVARCHAR(255) = NULL,
	@AuthAccessToken           NVARCHAR(255) = NULL,
	@AuthIdToken               NVARCHAR(1024) = NULL,
	@DateCreated               DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@EmailAddress              NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@CanTextMsg                NUMERIC(1,0) = 0,
	@DateTextMsgApproved       DATETIME = NULL,
	@AuthName                  NVARCHAR(255) = NULL,
	@AuthNickname              NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [user]
(
	[AuthUserid],
	[AuthConnection],
	[AuthProvider],
	[AuthAccessToken],
	[AuthIdToken],
	[date_created],
	[firstname],
	[middlename],
	[lastname],
	[phone_number],
	[email_address],
	[profileimageurl],
	[is_disabled],
	[can_text_msg],
	[date_text_msg_approved],
	[AuthName],
	[AuthNickname]
)
 VALUES 
(
	@AuthUserid,
	@AuthConnection,
	@AuthProvider,
	@AuthAccessToken,
	@AuthIdToken,
	@DateCreated,
	@Firstname,
	@Middlename,
	@Lastname,
	@PhoneNumber,
	@EmailAddress,
	@Profileimageurl,
	@IsDisabled,
	@CanTextMsg,
	@DateTextMsgApproved,
	@AuthName,
	@AuthNickname
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserEnum >>>>'
	DROP PROCEDURE [spUserEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		10/23/2016		HA		Created
*******************************************************************************/




CREATE PROCEDURE spUserEnum
	@UserID                    NUMERIC(10) = 0,
	@AuthUserid                NVARCHAR(255) = NULL,
	@AuthConnection            NVARCHAR(255) = NULL,
	@AuthProvider              NVARCHAR(255) = NULL,
	@AuthAccessToken           NVARCHAR(255) = NULL,
	@AuthIdToken               NVARCHAR(1024) = NULL,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@EmailAddress              NVARCHAR(255) = NULL,
	@Profileimageurl           NVARCHAR(255) = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@CanTextMsg                NUMERIC(1,0) = NULL,
    	@BeginDateTextMsgApproved  DATETIME = NULL,
    	@EndDateTextMsgApproved    DATETIME = NULL,
	@AuthName                  NVARCHAR(255) = NULL,
	@AuthNickname              NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [AuthUserid], [AuthConnection], [AuthProvider], [AuthAccessToken], [AuthIdToken], [date_created], [date_modified], [firstname], [middlename], [lastname], [phone_number], [email_address], [profileimageurl], [is_disabled], [can_text_msg], [date_text_msg_approved], [AuthName], [AuthNickname]
      FROM [user] 
      WHERE ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@AuthUserid IS NULL) OR ([AuthUserid] LIKE @AuthUserid))
      AND ((@AuthConnection IS NULL) OR ([AuthConnection] LIKE @AuthConnection))
      AND ((@AuthProvider IS NULL) OR ([AuthProvider] LIKE @AuthProvider))
      AND ((@AuthAccessToken IS NULL) OR ([AuthAccessToken] LIKE @AuthAccessToken))
      AND ((@AuthIdToken IS NULL) OR ([AuthIdToken] LIKE @AuthIdToken))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@Firstname IS NULL) OR ([firstname] LIKE @Firstname))
      AND ((@Middlename IS NULL) OR ([middlename] LIKE @Middlename))
      AND ((@Lastname IS NULL) OR ([lastname] LIKE @Lastname))
      AND ((@PhoneNumber IS NULL) OR ([phone_number] LIKE @PhoneNumber))
      AND ((@EmailAddress IS NULL) OR ([email_address] LIKE @EmailAddress))
      AND ((@Profileimageurl IS NULL) OR ([profileimageurl] LIKE @Profileimageurl))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
      AND ((@CanTextMsg IS NULL) OR ([can_text_msg] LIKE @CanTextMsg))
      AND ((@BeginDateTextMsgApproved IS NULL) OR ([date_text_msg_approved] >= @BeginDateTextMsgApproved))
      AND ((@EndDateTextMsgApproved IS NULL) OR ([date_text_msg_approved] <= @EndDateTextMsgApproved))
      AND ((@AuthName IS NULL) OR ([AuthName] LIKE @AuthName))
      AND ((@AuthNickname IS NULL) OR ([AuthNickname] LIKE @AuthNickname))
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum >>>'
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


