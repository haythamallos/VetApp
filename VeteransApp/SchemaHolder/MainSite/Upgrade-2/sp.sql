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

