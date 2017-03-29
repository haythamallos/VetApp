IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spJctUserContentTypeExist >>>>'
	DROP PROCEDURE [spJctUserContentTypeExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spJctUserContentTypeExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/24/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeExist
(
@JctUserContentTypeID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(jct_user_content_type_id) 
FROM [jct_user_content_type] 
WHERE jct_user_content_type_id = @JctUserContentTypeID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spJctUserContentTypeLoad >>>>'
	DROP PROCEDURE [spJctUserContentTypeLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spJctUserContentTypeLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/24/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeLoad
(
@JctUserContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [jct_user_content_type_id], [date_created], [date_modified], [user_id], [side_id], [content_type_id], [rating], [RatingLeft], [RatingRight] 
FROM [jct_user_content_type] 
WHERE jct_user_content_type_id = @JctUserContentTypeID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spJctUserContentTypeUpdate >>>>'
	DROP PROCEDURE [spJctUserContentTypeUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spJctUserContentTypeUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/24/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeUpdate
(
	@JctUserContentTypeID      NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@UserID                    NUMERIC(10) = 0,
	@SideID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@Rating                    NUMERIC(10,0) = 0,
	@RatingLeft                NUMERIC(10,0) = 0,
	@RatingRight               NUMERIC(10,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [jct_user_content_type] SET 
	[date_modified] = @DateModified,
	[user_id] = @UserID,
	[side_id] = @SideID,
	[content_type_id] = @ContentTypeID,
	[rating] = @Rating,
	[RatingLeft] = @RatingLeft,
	[RatingRight] = @RatingRight
WHERE jct_user_content_type_id = @JctUserContentTypeID

-- return ID for updated record
SELECT @PKID = @JctUserContentTypeID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spJctUserContentTypeDelete >>>>'
	DROP PROCEDURE [spJctUserContentTypeDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spJctUserContentTypeDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/24/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeDelete
(
@JctUserContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [jct_user_content_type] 
WHERE jct_user_content_type_id = @JctUserContentTypeID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spJctUserContentTypeInsert >>>>'
	DROP PROCEDURE [spJctUserContentTypeInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spJctUserContentTypeInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/24/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeInsert
(
	@JctUserContentTypeID      NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@UserID                    NUMERIC(10) = 0,
	@SideID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@Rating                    NUMERIC(10,0) = 0,
	@RatingLeft                NUMERIC(10,0) = 0,
	@RatingRight               NUMERIC(10,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [jct_user_content_type]
(
	[date_created],
	[user_id],
	[side_id],
	[content_type_id],
	[rating],
	[RatingLeft],
	[RatingRight]
)
 VALUES 
(
	@DateCreated,
	@UserID,
	@SideID,
	@ContentTypeID,
	@Rating,
	@RatingLeft,
	@RatingRight
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spJctUserContentTypeEnum >>>>'
	DROP PROCEDURE [spJctUserContentTypeEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spJctUserContentTypeEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/24/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spJctUserContentTypeEnum
	@JctUserContentTypeID      NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@UserID                    NUMERIC(10) = 0,
	@SideID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@Rating                    NUMERIC(10,0) = 0,
	@RatingLeft                NUMERIC(10,0) = 0,
	@RatingRight               NUMERIC(10,0) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [jct_user_content_type_id], [date_created], [date_modified], [user_id], [side_id], [content_type_id], [rating], [RatingLeft], [RatingRight]
      FROM [jct_user_content_type] 
      WHERE ((@JctUserContentTypeID = 0) OR ([jct_user_content_type_id] LIKE @JctUserContentTypeID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@SideID = 0) OR ([side_id] LIKE @SideID))
      AND ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
      AND ((@Rating = 0) OR ([rating] LIKE @Rating))
      AND ((@RatingLeft = 0) OR ([RatingLeft] LIKE @RatingLeft))
      AND ((@RatingRight = 0) OR ([RatingRight] LIKE @RatingRight))
 ORDER BY [jct_user_content_type_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeEnum >>>'
GO

