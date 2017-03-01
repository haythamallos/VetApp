IF EXISTS (select * from sysobjects where id = object_id(N'[spContentExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentExist >>>>'
	DROP PROCEDURE [spContentExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentExist
(
@ContentID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(content_id) 
FROM [content] 
WHERE content_id = @ContentID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentLoad >>>>'
	DROP PROCEDURE [spContentLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentLoad
(
@ContentID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_id], [user_id], [content_type_id], [date_created], [date_modified], [content_url], [content_data], [content_meta], [is_submitted], [is_disabled], [is_draft], [date_submitted], [notes] 
FROM [content] 
WHERE content_id = @ContentID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentUpdate >>>>'
	DROP PROCEDURE [spContentUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentUpdate
(
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsSubmitted               NUMERIC(1,0) = 0,
	@IsDisabled                NUMERIC(1,0) = 0,
	@IsDraft                   NUMERIC(1,0) = 0,
	@DateSubmitted             DATETIME = NULL,
	@Notes                     TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [content] SET 
	[user_id] = @UserID,
	[content_type_id] = @ContentTypeID,
	[date_modified] = @DateModified,
	[content_url] = @ContentUrl,
	[content_data] = @ContentData,
	[content_meta] = @ContentMeta,
	[is_submitted] = @IsSubmitted,
	[is_disabled] = @IsDisabled,
	[is_draft] = @IsDraft,
	[date_submitted] = @DateSubmitted,
	[notes] = @Notes
WHERE content_id = @ContentID

-- return ID for updated record
SELECT @PKID = @ContentID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentDelete >>>>'
	DROP PROCEDURE [spContentDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentDelete
(
@ContentID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [content] 
WHERE content_id = @ContentID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentInsert >>>>'
	DROP PROCEDURE [spContentInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentInsert
(
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsSubmitted               NUMERIC(1,0) = 0,
	@IsDisabled                NUMERIC(1,0) = 0,
	@IsDraft                   NUMERIC(1,0) = 0,
	@DateSubmitted             DATETIME = NULL,
	@Notes                     TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [content]
(
	[user_id],
	[content_type_id],
	[date_created],
	[content_url],
	[content_data],
	[content_meta],
	[is_submitted],
	[is_disabled],
	[is_draft],
	[date_submitted],
	[notes]
)
 VALUES 
(
	@UserID,
	@ContentTypeID,
	@DateCreated,
	@ContentUrl,
	@ContentData,
	@ContentMeta,
	@IsSubmitted,
	@IsDisabled,
	@IsDraft,
	@DateSubmitted,
	@Notes
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentEnum >>>>'
	DROP PROCEDURE [spContentEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spContentEnum
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsSubmitted               NUMERIC(1,0) = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@IsDraft                   NUMERIC(1,0) = NULL,
    	@BeginDateSubmitted        DATETIME = NULL,
    	@EndDateSubmitted          DATETIME = NULL,
	@Notes                     TEXT = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_id], [user_id], [content_type_id], [date_created], [date_modified], [content_url], [content_data], [content_meta], [is_submitted], [is_disabled], [is_draft], [date_submitted], [notes]
      FROM [content] 
      WHERE ((@ContentID = 0) OR ([content_id] LIKE @ContentID))
      AND ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@ContentUrl IS NULL) OR ([content_url] LIKE @ContentUrl))
      AND ((@ContentData IS NULL) OR ([content_data] LIKE @ContentData))
      AND ((@ContentMeta IS NULL) OR ([content_meta] LIKE @ContentMeta))
      AND ((@IsSubmitted IS NULL) OR ([is_submitted] LIKE @IsSubmitted))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
      AND ((@IsDraft IS NULL) OR ([is_draft] LIKE @IsDraft))
      AND ((@BeginDateSubmitted IS NULL) OR ([date_submitted] >= @BeginDateSubmitted))
      AND ((@EndDateSubmitted IS NULL) OR ([date_submitted] <= @EndDateSubmitted))
      AND ((@Notes IS NULL) OR ([notes] LIKE @Notes))
 ORDER BY [content_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentEnum >>>'
GO



IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentTypeExist >>>>'
	DROP PROCEDURE [spContentTypeExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentTypeExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeExist
(
@ContentTypeID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(content_type_id) 
FROM [content_type] 
WHERE content_type_id = @ContentTypeID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentTypeLoad >>>>'
	DROP PROCEDURE [spContentTypeLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentTypeLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeLoad
(
@ContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_type_id], [date_created], [code], [description], [visible_code] 
FROM [content_type] 
WHERE content_type_id = @ContentTypeID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentTypeUpdate >>>>'
	DROP PROCEDURE [spContentTypeUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentTypeUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeUpdate
(
	@ContentTypeID             NUMERIC(10) = 0,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [content_type] SET 
	[code] = @Code,
	[description] = @Description,
	[visible_code] = @VisibleCode
WHERE content_type_id = @ContentTypeID

-- return ID for updated record
SELECT @PKID = @ContentTypeID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentTypeDelete >>>>'
	DROP PROCEDURE [spContentTypeDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentTypeDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeDelete
(
@ContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [content_type] 
WHERE content_type_id = @ContentTypeID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentTypeInsert >>>>'
	DROP PROCEDURE [spContentTypeInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentTypeInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeInsert
(
	@ContentTypeID             NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [content_type]
(
	[content_type_id],
	[date_created],
	[code],
	[description],
	[visible_code]
)
 VALUES 
(
	@ContentTypeID,
	@DateCreated,
	@Code,
	@Description,
	@VisibleCode
)


-- return ID for new record
SELECT @PKID = @ContentTypeID

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentTypeEnum >>>>'
	DROP PROCEDURE [spContentTypeEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentTypeEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/1/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spContentTypeEnum
	@ContentTypeID             NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_type_id], [date_created], [code], [description], [visible_code]
      FROM [content_type] 
      WHERE ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Code IS NULL) OR ([code] LIKE @Code))
      AND ((@Description IS NULL) OR ([description] LIKE @Description))
      AND ((@VisibleCode IS NULL) OR ([visible_code] LIKE @VisibleCode))
 ORDER BY [content_type_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeEnum >>>'
GO

