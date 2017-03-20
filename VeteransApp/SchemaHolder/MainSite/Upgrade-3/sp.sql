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
**		3/20/2017		HA		Created
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
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeLoad
(
@ContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_type_id], [date_created], [code], [description], [visible_code], [max_rating], [has_sides], [price], [product_ref_name], [product_ref_description], [number_of_pages] 
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
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeUpdate
(
	@ContentTypeID             NUMERIC(10) = 0,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@MaxRating                 NUMERIC(10,0) = 0,
	@HasSides                  NUMERIC(1,0) = 0,
	@Price                     NUMERIC(10,2) = 0,
	@ProductRefName            NVARCHAR(255) = NULL,
	@ProductRefDescription     NVARCHAR(255) = NULL,
	@NumberOfPages             NUMERIC(10,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [content_type] SET 
	[code] = @Code,
	[description] = @Description,
	[visible_code] = @VisibleCode,
	[max_rating] = @MaxRating,
	[has_sides] = @HasSides,
	[price] = @Price,
	[product_ref_name] = @ProductRefName,
	[product_ref_description] = @ProductRefDescription,
	[number_of_pages] = @NumberOfPages
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
**		3/20/2017		HA		Created
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
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeInsert
(
	@ContentTypeID             NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@MaxRating                 NUMERIC(10,0) = 0,
	@HasSides                  NUMERIC(1,0) = 0,
	@Price                     NUMERIC(10,2) = 0,
	@ProductRefName            NVARCHAR(255) = NULL,
	@ProductRefDescription     NVARCHAR(255) = NULL,
	@NumberOfPages             NUMERIC(10,0) = 0,
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
	[visible_code],
	[max_rating],
	[has_sides],
	[price],
	[product_ref_name],
	[product_ref_description],
	[number_of_pages]
)
 VALUES 
(
	@ContentTypeID,
	@DateCreated,
	@Code,
	@Description,
	@VisibleCode,
	@MaxRating,
	@HasSides,
	@Price,
	@ProductRefName,
	@ProductRefDescription,
	@NumberOfPages
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
**		3/20/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spContentTypeEnum
	@ContentTypeID             NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@MaxRating                 NUMERIC(10,0) = 0,
	@HasSides                  NUMERIC(1,0) = NULL,
	@Price                     NUMERIC(10,2) = 0,
    	@BeginPrice                NUMERIC(10,2) = 0,
    	@EndPrice                  NUMERIC(10,2) = 0,
	@ProductRefName            NVARCHAR(255) = NULL,
	@ProductRefDescription     NVARCHAR(255) = NULL,
	@NumberOfPages             NUMERIC(10,0) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_type_id], [date_created], [code], [description], [visible_code], [max_rating], [has_sides], [price], [product_ref_name], [product_ref_description], [number_of_pages]
      FROM [content_type] 
      WHERE ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Code IS NULL) OR ([code] LIKE @Code))
      AND ((@Description IS NULL) OR ([description] LIKE @Description))
      AND ((@VisibleCode IS NULL) OR ([visible_code] LIKE @VisibleCode))
      AND ((@MaxRating = 0) OR ([max_rating] LIKE @MaxRating))
      AND ((@HasSides IS NULL) OR ([has_sides] LIKE @HasSides))
      AND ((@BeginPrice = 0) OR ([price] >= @BeginPrice))
      AND ((@EndPrice = 0) OR ([price] <= @EndPrice))
      AND ((@ProductRefName IS NULL) OR ([product_ref_name] LIKE @ProductRefName))
      AND ((@ProductRefDescription IS NULL) OR ([product_ref_description] LIKE @ProductRefDescription))
      AND ((@NumberOfPages = 0) OR ([number_of_pages] LIKE @NumberOfPages))
 ORDER BY [content_type_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentTypeEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentTypeEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentTypeEnum >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spCartItemExist >>>>'
	DROP PROCEDURE [spCartItemExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spCartItemExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spCartItemExist
(
@CartItemID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(cart_item_id) 
FROM [cart_item] 
WHERE cart_item_id = @CartItemID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spCartItemExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spCartItemExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spCartItemLoad >>>>'
	DROP PROCEDURE [spCartItemLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spCartItemLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spCartItemLoad
(
@CartItemID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [cart_item_id], [date_created], [date_modified], [purchase_id], [user_id], [content_id], [content_type_id] 
FROM [cart_item] 
WHERE cart_item_id = @CartItemID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spCartItemLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spCartItemLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spCartItemUpdate >>>>'
	DROP PROCEDURE [spCartItemUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spCartItemUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spCartItemUpdate
(
	@CartItemID                NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@PurchaseID                NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@ContentID                 NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [cart_item] SET 
	[date_modified] = @DateModified,
	[purchase_id] = @PurchaseID,
	[user_id] = @UserID,
	[content_id] = @ContentID,
	[content_type_id] = @ContentTypeID
WHERE cart_item_id = @CartItemID

-- return ID for updated record
SELECT @PKID = @CartItemID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spCartItemUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spCartItemUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spCartItemDelete >>>>'
	DROP PROCEDURE [spCartItemDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spCartItemDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spCartItemDelete
(
@CartItemID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [cart_item] 
WHERE cart_item_id = @CartItemID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spCartItemDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spCartItemDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spCartItemInsert >>>>'
	DROP PROCEDURE [spCartItemInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spCartItemInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/20/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spCartItemInsert
(
	@CartItemID                NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@PurchaseID                NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@ContentID                 NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [cart_item]
(
	[date_created],
	[purchase_id],
	[user_id],
	[content_id],
	[content_type_id]
)
 VALUES 
(
	@DateCreated,
	@PurchaseID,
	@UserID,
	@ContentID,
	@ContentTypeID
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spCartItemInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spCartItemInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spCartItemEnum >>>>'
	DROP PROCEDURE [spCartItemEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spCartItemEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/20/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spCartItemEnum
	@CartItemID                NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@PurchaseID                NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@ContentID                 NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [cart_item_id], [date_created], [date_modified], [purchase_id], [user_id], [content_id], [content_type_id]
      FROM [cart_item] 
      WHERE ((@CartItemID = 0) OR ([cart_item_id] LIKE @CartItemID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@PurchaseID = 0) OR ([purchase_id] LIKE @PurchaseID))
      AND ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@ContentID = 0) OR ([content_id] LIKE @ContentID))
      AND ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
 ORDER BY [cart_item_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spCartItemEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spCartItemEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spCartItemEnum >>>'
GO

