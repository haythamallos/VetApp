/*******************************************************************************
**		PROCEDURE NAME: spContentEnum1 (CUSTOM)
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentEnum1 >>>>'
	DROP PROCEDURE spContentEnum1
END
GO

CREATE PROCEDURE spContentEnum1
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@PurchaseID                NUMERIC(10) = 0,
	@ContentStateID            NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@Notes                     TEXT = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url],[is_disabled]
      FROM [content] 
      WHERE ([user_id] = @UserID) and ([is_disabled] = @IsDisabled)
	 ORDER BY [content_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentEnum1 >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentEnum1 >>>'
GO




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
**		3/11/2017		HA		Created
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
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentLoad
(
@ContentID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url], [content_data], [content_meta], [is_disabled], [notes] 
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
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentUpdate
(
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@PurchaseID                NUMERIC(10) = 0,
	@ContentStateID            NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@Notes                     TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [content] SET 
	[user_id] = @UserID,
	[purchase_id] = @PurchaseID,
	[content_state_id] = @ContentStateID,
	[content_type_id] = @ContentTypeID,
	[date_modified] = @DateModified,
	[content_url] = @ContentUrl,
	[content_data] = @ContentData,
	[content_meta] = @ContentMeta,
	[is_disabled] = @IsDisabled,
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
**		3/11/2017		HA		Created
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
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentInsert
(
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@PurchaseID                NUMERIC(10) = 0,
	@ContentStateID            NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@Notes                     TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [content]
(
	[user_id],
	[purchase_id],
	[content_state_id],
	[content_type_id],
	[date_created],
	[content_url],
	[content_data],
	[content_meta],
	[is_disabled],
	[notes]
)
 VALUES 
(
	@UserID,
	@PurchaseID,
	@ContentStateID,
	@ContentTypeID,
	@DateCreated,
	@ContentUrl,
	@ContentData,
	@ContentMeta,
	@IsDisabled,
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
**		3/11/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spContentEnum
	@ContentID                 NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@PurchaseID                NUMERIC(10) = 0,
	@ContentStateID            NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@ContentUrl                NVARCHAR(255) = NULL,
	@ContentData               VARBINARY(MAX) = NULL,
	@ContentMeta               TEXT = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@Notes                     TEXT = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url], [content_data], [content_meta], [is_disabled], [notes]
      FROM [content] 
      WHERE ((@ContentID = 0) OR ([content_id] LIKE @ContentID))
      AND ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@PurchaseID = 0) OR ([purchase_id] LIKE @PurchaseID))
      AND ((@ContentStateID = 0) OR ([content_state_id] LIKE @ContentStateID))
      AND ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@ContentUrl IS NULL) OR ([content_url] LIKE @ContentUrl))
      AND ((@ContentData IS NULL) OR ([content_data] LIKE @ContentData))
      AND ((@ContentMeta IS NULL) OR ([content_meta] LIKE @ContentMeta))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
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


IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentStateExist >>>>'
	DROP PROCEDURE [spContentStateExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentStateExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentStateExist
(
@ContentStateID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(content_state_id) 
FROM [content_state] 
WHERE content_state_id = @ContentStateID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentStateExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentStateExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentStateLoad >>>>'
	DROP PROCEDURE [spContentStateLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentStateLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentStateLoad
(
@ContentStateID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_state_id], [date_created], [code], [description], [visible_code] 
FROM [content_state] 
WHERE content_state_id = @ContentStateID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentStateLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentStateLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentStateUpdate >>>>'
	DROP PROCEDURE [spContentStateUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentStateUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentStateUpdate
(
	@ContentStateID            NUMERIC(10) = 0,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [content_state] SET 
	[code] = @Code,
	[description] = @Description,
	[visible_code] = @VisibleCode
WHERE content_state_id = @ContentStateID

-- return ID for updated record
SELECT @PKID = @ContentStateID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentStateUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentStateUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentStateDelete >>>>'
	DROP PROCEDURE [spContentStateDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentStateDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentStateDelete
(
@ContentStateID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [content_state] 
WHERE content_state_id = @ContentStateID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentStateDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentStateDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentStateInsert >>>>'
	DROP PROCEDURE [spContentStateInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentStateInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentStateInsert
(
	@ContentStateID            NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [content_state]
(
	[content_state_id],
	[date_created],
	[code],
	[description],
	[visible_code]
)
 VALUES 
(
	@ContentStateID,
	@DateCreated,
	@Code,
	@Description,
	@VisibleCode
)


-- return ID for new record
SELECT @PKID = @ContentStateID

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentStateInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentStateInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentStateEnum >>>>'
	DROP PROCEDURE [spContentStateEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spContentStateEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/11/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spContentStateEnum
	@ContentStateID            NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_state_id], [date_created], [code], [description], [visible_code]
      FROM [content_state] 
      WHERE ((@ContentStateID = 0) OR ([content_state_id] LIKE @ContentStateID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Code IS NULL) OR ([code] LIKE @Code))
      AND ((@Description IS NULL) OR ([description] LIKE @Description))
      AND ((@VisibleCode IS NULL) OR ([visible_code] LIKE @VisibleCode))
 ORDER BY [content_state_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentStateEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentStateEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentStateEnum >>>'
GO

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
**		3/16/2017		HA		Created
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
**		3/16/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserLoad
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck] 
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
**		3/16/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserUpdate
(
	@UserID                    NUMERIC(10) = 0,
	@UserRoleID                NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@Fullname                  NVARCHAR(255) = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@Username                  NVARCHAR(255) = NULL,
	@Passwd                    NVARCHAR(255) = NULL,
	@Ssn                       NVARCHAR(255) = NULL,
	@PictureUrl                NVARCHAR(255) = NULL,
	@Picture                   VARBINARY(MAX) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@WelcomeEmailSent          NUMERIC(1,0) = 0,
	@Validationtoken           NVARCHAR(255) = NULL,
	@Validationlink            NVARCHAR(255) = NULL,
	@Isvalidated               NUMERIC(1,0) = 0,
	@WelcomeEmailSentDate      DATETIME = NULL,
	@LastLoginDate             DATETIME = NULL,
	@InternalNotes             TEXT = NULL,
	@UserMessage               TEXT = NULL,
	@CookieID                  NVARCHAR(255) = NULL,
	@CurrentRating             NUMERIC(10,0) = 0,
	@SecurityQuestion          NVARCHAR(255) = NULL,
	@SecurityAnswer            NVARCHAR(255) = NULL,
	@NumberOfVisits            NUMERIC(10,0) = 0,
	@PreviousVisitDate         DATETIME = NULL,
	@LastVisitDate             DATETIME = NULL,
	@CurrentRatingBack         NUMERIC(10,0) = 0,
	@CurrentRatingShoulder     NUMERIC(10,0) = 0,
	@CurrentRatingNeck         NUMERIC(10,0) = 0,
	@HasRatingBack             NUMERIC(1,0) = 0,
	@HasRatingShoulder         NUMERIC(1,0) = 0,
	@HasRatingNeck             NUMERIC(1,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [user] SET 
	[user_role_id] = @UserRoleID,
	[date_modified] = @DateModified,
	[fullname] = @Fullname,
	[firstname] = @Firstname,
	[middlename] = @Middlename,
	[lastname] = @Lastname,
	[phone_number] = @PhoneNumber,
	[username] = @Username,
	[passwd] = @Passwd,
	[ssn] = @Ssn,
	[picture_url] = @PictureUrl,
	[picture] = @Picture,
	[is_disabled] = @IsDisabled,
	[welcome_email_sent] = @WelcomeEmailSent,
	[validationtoken] = @Validationtoken,
	[validationlink] = @Validationlink,
	[isvalidated] = @Isvalidated,
	[welcome_email_sent_date] = @WelcomeEmailSentDate,
	[last_login_date] = @LastLoginDate,
	[internal_notes] = @InternalNotes,
	[user_message] = @UserMessage,
	[cookie_id] = @CookieID,
	[current_rating] = @CurrentRating,
	[security_question] = @SecurityQuestion,
	[security_answer] = @SecurityAnswer,
	[number_of_visits] = @NumberOfVisits,
	[previous_visit_date] = @PreviousVisitDate,
	[last_visit_date] = @LastVisitDate,
	[current_rating_back] = @CurrentRatingBack,
	[current_rating_shoulder] = @CurrentRatingShoulder,
	[current_rating_neck] = @CurrentRatingNeck,
	[has_rating_back] = @HasRatingBack,
	[has_rating_shoulder] = @HasRatingShoulder,
	[has_rating_neck] = @HasRatingNeck
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
**		3/16/2017		HA		Created
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
**		3/16/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserInsert
(
	@UserID                    NUMERIC(10) = 0,
	@UserRoleID                NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Fullname                  NVARCHAR(255) = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@Username                  NVARCHAR(255) = NULL,
	@Passwd                    NVARCHAR(255) = NULL,
	@Ssn                       NVARCHAR(255) = NULL,
	@PictureUrl                NVARCHAR(255) = NULL,
	@Picture                   VARBINARY(MAX) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@WelcomeEmailSent          NUMERIC(1,0) = 0,
	@Validationtoken           NVARCHAR(255) = NULL,
	@Validationlink            NVARCHAR(255) = NULL,
	@Isvalidated               NUMERIC(1,0) = 0,
	@WelcomeEmailSentDate      DATETIME = NULL,
	@LastLoginDate             DATETIME = NULL,
	@InternalNotes             TEXT = NULL,
	@UserMessage               TEXT = NULL,
	@CookieID                  NVARCHAR(255) = NULL,
	@CurrentRating             NUMERIC(10,0) = 0,
	@SecurityQuestion          NVARCHAR(255) = NULL,
	@SecurityAnswer            NVARCHAR(255) = NULL,
	@NumberOfVisits            NUMERIC(10,0) = 0,
	@PreviousVisitDate         DATETIME = NULL,
	@LastVisitDate             DATETIME = NULL,
	@CurrentRatingBack         NUMERIC(10,0) = 0,
	@CurrentRatingShoulder     NUMERIC(10,0) = 0,
	@CurrentRatingNeck         NUMERIC(10,0) = 0,
	@HasRatingBack             NUMERIC(1,0) = 0,
	@HasRatingShoulder         NUMERIC(1,0) = 0,
	@HasRatingNeck             NUMERIC(1,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [user]
(
	[user_role_id],
	[date_created],
	[fullname],
	[firstname],
	[middlename],
	[lastname],
	[phone_number],
	[username],
	[passwd],
	[ssn],
	[picture_url],
	[picture],
	[is_disabled],
	[welcome_email_sent],
	[validationtoken],
	[validationlink],
	[isvalidated],
	[welcome_email_sent_date],
	[last_login_date],
	[internal_notes],
	[user_message],
	[cookie_id],
	[current_rating],
	[security_question],
	[security_answer],
	[number_of_visits],
	[previous_visit_date],
	[last_visit_date],
	[current_rating_back],
	[current_rating_shoulder],
	[current_rating_neck],
	[has_rating_back],
	[has_rating_shoulder],
	[has_rating_neck]
)
 VALUES 
(
	@UserRoleID,
	@DateCreated,
	@Fullname,
	@Firstname,
	@Middlename,
	@Lastname,
	@PhoneNumber,
	@Username,
	@Passwd,
	@Ssn,
	@PictureUrl,
	@Picture,
	@IsDisabled,
	@WelcomeEmailSent,
	@Validationtoken,
	@Validationlink,
	@Isvalidated,
	@WelcomeEmailSentDate,
	@LastLoginDate,
	@InternalNotes,
	@UserMessage,
	@CookieID,
	@CurrentRating,
	@SecurityQuestion,
	@SecurityAnswer,
	@NumberOfVisits,
	@PreviousVisitDate,
	@LastVisitDate,
	@CurrentRatingBack,
	@CurrentRatingShoulder,
	@CurrentRatingNeck,
	@HasRatingBack,
	@HasRatingShoulder,
	@HasRatingNeck
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
**		3/16/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spUserEnum
	@UserID                    NUMERIC(10) = 0,
	@UserRoleID                NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Fullname                  NVARCHAR(255) = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@Username                  NVARCHAR(255) = NULL,
	@Passwd                    NVARCHAR(255) = NULL,
	@Ssn                       NVARCHAR(255) = NULL,
	@PictureUrl                NVARCHAR(255) = NULL,
	@Picture                   VARBINARY(MAX) = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
	@WelcomeEmailSent          NUMERIC(1,0) = NULL,
	@Validationtoken           NVARCHAR(255) = NULL,
	@Validationlink            NVARCHAR(255) = NULL,
	@Isvalidated               NUMERIC(1,0) = NULL,
    	@BeginWelcomeEmailSentDate DATETIME = NULL,
    	@EndWelcomeEmailSentDate   DATETIME = NULL,
    	@BeginLastLoginDate        DATETIME = NULL,
    	@EndLastLoginDate          DATETIME = NULL,
	@InternalNotes             TEXT = NULL,
	@UserMessage               TEXT = NULL,
	@CookieID                  NVARCHAR(255) = NULL,
	@CurrentRating             NUMERIC(10,0) = 0,
	@SecurityQuestion          NVARCHAR(255) = NULL,
	@SecurityAnswer            NVARCHAR(255) = NULL,
	@NumberOfVisits            NUMERIC(10,0) = 0,
    	@BeginPreviousVisitDate    DATETIME = NULL,
    	@EndPreviousVisitDate      DATETIME = NULL,
    	@BeginLastVisitDate        DATETIME = NULL,
    	@EndLastVisitDate          DATETIME = NULL,
	@CurrentRatingBack         NUMERIC(10,0) = 0,
	@CurrentRatingShoulder     NUMERIC(10,0) = 0,
	@CurrentRatingNeck         NUMERIC(10,0) = 0,
	@HasRatingBack             NUMERIC(1,0) = NULL,
	@HasRatingShoulder         NUMERIC(1,0) = NULL,
	@HasRatingNeck             NUMERIC(1,0) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


 IF(1 = 1)
      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
      FROM [user] 
      WHERE ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@UserRoleID = 0) OR ([user_role_id] LIKE @UserRoleID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@Fullname IS NULL) OR ([fullname] LIKE @Fullname))
      AND ((@Firstname IS NULL) OR ([firstname] LIKE @Firstname))
      AND ((@Middlename IS NULL) OR ([middlename] LIKE @Middlename))
      AND ((@Lastname IS NULL) OR ([lastname] LIKE @Lastname))
      AND ((@PhoneNumber IS NULL) OR ([phone_number] LIKE @PhoneNumber))
      AND ((@Username IS NULL) OR ([username] LIKE @Username))
      AND ((@Passwd IS NULL) OR ([passwd] LIKE @Passwd))
      AND ((@Ssn IS NULL) OR ([ssn] LIKE @Ssn))
      AND ((@PictureUrl IS NULL) OR ([picture_url] LIKE @PictureUrl))
      AND ((@Picture IS NULL) OR ([picture] LIKE @Picture))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
      AND ((@WelcomeEmailSent IS NULL) OR ([welcome_email_sent] LIKE @WelcomeEmailSent))
      AND ((@Validationtoken IS NULL) OR ([validationtoken] LIKE @Validationtoken))
      AND ((@Validationlink IS NULL) OR ([validationlink] LIKE @Validationlink))
      AND ((@Isvalidated IS NULL) OR ([isvalidated] LIKE @Isvalidated))
      AND ((@BeginWelcomeEmailSentDate IS NULL) OR ([welcome_email_sent_date] >= @BeginWelcomeEmailSentDate))
      AND ((@EndWelcomeEmailSentDate IS NULL) OR ([welcome_email_sent_date] <= @EndWelcomeEmailSentDate))
      AND ((@BeginLastLoginDate IS NULL) OR ([last_login_date] >= @BeginLastLoginDate))
      AND ((@EndLastLoginDate IS NULL) OR ([last_login_date] <= @EndLastLoginDate))
      AND ((@InternalNotes IS NULL) OR ([internal_notes] LIKE @InternalNotes))
      AND ((@UserMessage IS NULL) OR ([user_message] LIKE @UserMessage))
      AND ((@CookieID IS NULL) OR ([cookie_id] LIKE @CookieID))
      AND ((@CurrentRating = 0) OR ([current_rating] LIKE @CurrentRating))
      AND ((@SecurityQuestion IS NULL) OR ([security_question] LIKE @SecurityQuestion))
      AND ((@SecurityAnswer IS NULL) OR ([security_answer] LIKE @SecurityAnswer))
      AND ((@NumberOfVisits = 0) OR ([number_of_visits] LIKE @NumberOfVisits))
      AND ((@BeginPreviousVisitDate IS NULL) OR ([previous_visit_date] >= @BeginPreviousVisitDate))
      AND ((@EndPreviousVisitDate IS NULL) OR ([previous_visit_date] <= @EndPreviousVisitDate))
      AND ((@BeginLastVisitDate IS NULL) OR ([last_visit_date] >= @BeginLastVisitDate))
      AND ((@EndLastVisitDate IS NULL) OR ([last_visit_date] <= @EndLastVisitDate))
      AND ((@CurrentRatingBack = 0) OR ([current_rating_back] LIKE @CurrentRatingBack))
      AND ((@CurrentRatingShoulder = 0) OR ([current_rating_shoulder] LIKE @CurrentRatingShoulder))
      AND ((@CurrentRatingNeck = 0) OR ([current_rating_neck] LIKE @CurrentRatingNeck))
      AND ((@HasRatingBack IS NULL) OR ([has_rating_back] LIKE @HasRatingBack))
      AND ((@HasRatingShoulder IS NULL) OR ([has_rating_shoulder] LIKE @HasRatingShoulder))
      AND ((@HasRatingNeck IS NULL) OR ([has_rating_neck] LIKE @HasRatingNeck))
 ORDER BY [user_id] ASC
	ELSE 	IF (@UserID > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [user_id] = @UserID
	ELSE 	IF (@UserRoleID > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [user_role_id] = @UserRoleID
	ELSE 	IF  (@BeginDateCreated is not NULL) and
	      (@EndDateCreated is not NULL)  
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [date_created] >= @BeginDateCreated and
			[date_created] <= @EndDateCreated

	ELSE 	IF  (@BeginDateModified is not NULL) and
	      (@EndDateModified is not NULL)  
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [date_modified] >= @BeginDateModified and
			[date_modified] <= @EndDateModified

	ELSE 	IF   (@Fullname is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [fullname] LIKE @Fullname
	ELSE 	IF   (@Firstname is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [firstname] LIKE @Firstname
	ELSE 	IF   (@Middlename is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [middlename] LIKE @Middlename
	ELSE 	IF   (@Lastname is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [lastname] LIKE @Lastname
	ELSE 	IF   (@PhoneNumber is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [phone_number] LIKE @PhoneNumber
	ELSE 	IF   (@Username is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [username] LIKE @Username
	ELSE 	IF   (@Passwd is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [passwd] LIKE @Passwd
	ELSE 	IF   (@Ssn is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [ssn] LIKE @Ssn
	ELSE 	IF   (@PictureUrl is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [picture_url] LIKE @PictureUrl
	ELSE 	IF   (@Picture is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [picture] LIKE @Picture
	ELSE 	IF (@IsDisabled is not NULL )
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [is_disabled] = @IsDisabled
	ELSE 	IF (@WelcomeEmailSent is not NULL )
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [welcome_email_sent] = @WelcomeEmailSent
	ELSE 	IF   (@Validationtoken is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [validationtoken] LIKE @Validationtoken
	ELSE 	IF   (@Validationlink is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [validationlink] LIKE @Validationlink
	ELSE 	IF (@Isvalidated is not NULL )
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [isvalidated] = @Isvalidated
	ELSE 	IF  (@BeginWelcomeEmailSentDate is not NULL) and
	      (@EndWelcomeEmailSentDate is not NULL)  
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [welcome_email_sent_date] >= @BeginWelcomeEmailSentDate and
			[welcome_email_sent_date] <= @EndWelcomeEmailSentDate

	ELSE 	IF  (@BeginLastLoginDate is not NULL) and
	      (@EndLastLoginDate is not NULL)  
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [last_login_date] >= @BeginLastLoginDate and
			[last_login_date] <= @EndLastLoginDate

	ELSE 	IF   (@InternalNotes is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [internal_notes] LIKE @InternalNotes
	ELSE 	IF   (@UserMessage is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [user_message] LIKE @UserMessage
	ELSE 	IF   (@CookieID is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [cookie_id] LIKE @CookieID
	ELSE 	IF (@CurrentRating > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [current_rating] = @CurrentRating
	ELSE 	IF   (@SecurityQuestion is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [security_question] LIKE @SecurityQuestion
	ELSE 	IF   (@SecurityAnswer is not NULL)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [security_answer] LIKE @SecurityAnswer
	ELSE 	IF (@NumberOfVisits > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [number_of_visits] = @NumberOfVisits
	ELSE 	IF  (@BeginPreviousVisitDate is not NULL) and
	      (@EndPreviousVisitDate is not NULL)  
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [previous_visit_date] >= @BeginPreviousVisitDate and
			[previous_visit_date] <= @EndPreviousVisitDate

	ELSE 	IF  (@BeginLastVisitDate is not NULL) and
	      (@EndLastVisitDate is not NULL)  
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [last_visit_date] >= @BeginLastVisitDate and
			[last_visit_date] <= @EndLastVisitDate

	ELSE 	IF (@CurrentRatingBack > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [current_rating_back] = @CurrentRatingBack
	ELSE 	IF (@CurrentRatingShoulder > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [current_rating_shoulder] = @CurrentRatingShoulder
	ELSE 	IF (@CurrentRatingNeck > 0)
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [current_rating_neck] = @CurrentRatingNeck
	ELSE 	IF (@HasRatingBack is not NULL )
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [has_rating_back] = @HasRatingBack
	ELSE 	IF (@HasRatingShoulder is not NULL )
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [has_rating_shoulder] = @HasRatingShoulder
	ELSE 	IF (@HasRatingNeck is not NULL )
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 
		WHERE [has_rating_neck] = @HasRatingNeck
	ELSE
		SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [current_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [current_rating_back], [current_rating_shoulder], [current_rating_neck], [has_rating_back], [has_rating_shoulder], [has_rating_neck]
		FROM [user] 

	SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum >>>'
GO

