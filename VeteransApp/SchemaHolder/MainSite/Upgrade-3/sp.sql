/*******************************************************************************
**		CUSTOM SP BEGIN
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
	@Authtoken                 NVARCHAR(255) = NULL,
	@ErrorPurchaseID           NUMERIC(10) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url], [content_meta], [is_disabled], [notes], [authtoken], [error_purchase_id]
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


IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spContentEnum2 >>>>'
	DROP PROCEDURE spContentEnum2
END
GO

CREATE PROCEDURE spContentEnum2
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
	@Authtoken                 NVARCHAR(255) = NULL,
	@ErrorPurchaseID           NUMERIC(10) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url], [content_meta], [is_disabled], [notes], [authtoken], [error_purchase_id]
      FROM [content] 
      WHERE ([user_id] = @UserID) and ([is_disabled] = @IsDisabled) and ([content_state_id] = 7 or [content_state_id] = 8)
	  and ([purchase_id] > 0)

 ORDER BY [content_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentEnum2 >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentEnum2 >>>'
GO

/*******************************************************************************
**		CUSTOM SP END
*******************************************************************************/

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
**		3/21/2017		HA		Created
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
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeLoad
(
@ContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_type_id], [date_created], [code], [description], [visible_code], [max_rating], [has_sides], [product_ref_name], [product_ref_description], [number_of_pages], [price_in_pennies] 
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
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentTypeUpdate
(
	@ContentTypeID             NUMERIC(10) = 0,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@MaxRating                 NUMERIC(10,0) = 0,
	@HasSides                  NUMERIC(1,0) = 0,
	@ProductRefName            NVARCHAR(255) = NULL,
	@ProductRefDescription     NVARCHAR(255) = NULL,
	@NumberOfPages             NUMERIC(10,0) = 0,
	@PriceInPennies            NUMERIC(10,0) = 0,
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
	[product_ref_name] = @ProductRefName,
	[product_ref_description] = @ProductRefDescription,
	[number_of_pages] = @NumberOfPages,
	[price_in_pennies] = @PriceInPennies
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
**		3/21/2017		HA		Created
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
**		3/21/2017		HA		Created
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
	@ProductRefName            NVARCHAR(255) = NULL,
	@ProductRefDescription     NVARCHAR(255) = NULL,
	@NumberOfPages             NUMERIC(10,0) = 0,
	@PriceInPennies            NUMERIC(10,0) = 0,
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
	[product_ref_name],
	[product_ref_description],
	[number_of_pages],
	[price_in_pennies]
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
	@ProductRefName,
	@ProductRefDescription,
	@NumberOfPages,
	@PriceInPennies
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
**		3/21/2017		HA		Created
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
	@ProductRefName            NVARCHAR(255) = NULL,
	@ProductRefDescription     NVARCHAR(255) = NULL,
	@NumberOfPages             NUMERIC(10,0) = 0,
	@PriceInPennies            NUMERIC(10,0) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_type_id], [date_created], [code], [description], [visible_code], [max_rating], [has_sides], [product_ref_name], [product_ref_description], [number_of_pages], [price_in_pennies]
      FROM [content_type] 
      WHERE ((@ContentTypeID = 0) OR ([content_type_id] LIKE @ContentTypeID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Code IS NULL) OR ([code] LIKE @Code))
      AND ((@Description IS NULL) OR ([description] LIKE @Description))
      AND ((@VisibleCode IS NULL) OR ([visible_code] LIKE @VisibleCode))
      AND ((@MaxRating = 0) OR ([max_rating] LIKE @MaxRating))
      AND ((@HasSides IS NULL) OR ([has_sides] LIKE @HasSides))
      AND ((@ProductRefName IS NULL) OR ([product_ref_name] LIKE @ProductRefName))
      AND ((@ProductRefDescription IS NULL) OR ([product_ref_description] LIKE @ProductRefDescription))
      AND ((@NumberOfPages = 0) OR ([number_of_pages] LIKE @NumberOfPages))
      AND ((@PriceInPennies = 0) OR ([price_in_pennies] LIKE @PriceInPennies))
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
**		3/21/2017		HA		Created
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
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spContentLoad
(
@ContentID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url], [content_data], [content_meta], [is_disabled], [notes], [authtoken], [error_purchase_id] 
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
**		3/21/2017		HA		Created
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
	@Authtoken                 NVARCHAR(255) = NULL,
	@ErrorPurchaseID           NUMERIC(10) = 0,
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
	[notes] = @Notes,
	[authtoken] = @Authtoken,
	[error_purchase_id] = @ErrorPurchaseID
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
**		3/21/2017		HA		Created
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
**		3/21/2017		HA		Created
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
	@Authtoken                 NVARCHAR(255) = NULL,
	@ErrorPurchaseID           NUMERIC(10) = 0,
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
	[notes],
	[authtoken],
	[error_purchase_id]
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
	@Notes,
	@Authtoken,
	@ErrorPurchaseID
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
**		3/21/2017		HA		Created
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
	@Authtoken                 NVARCHAR(255) = NULL,
	@ErrorPurchaseID           NUMERIC(10) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [content_id], [user_id], [purchase_id], [content_state_id], [content_type_id], [date_created], [date_modified], [content_url], [content_data], [content_meta], [is_disabled], [notes], [authtoken], [error_purchase_id]
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
      AND ((@Authtoken IS NULL) OR ([authtoken] LIKE @Authtoken))
      AND ((@ErrorPurchaseID = 0) OR ([error_purchase_id] LIKE @ErrorPurchaseID))
 ORDER BY [content_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spContentEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spContentEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spContentEnum >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spPurchaseExist >>>>'
	DROP PROCEDURE [spPurchaseExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spPurchaseExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spPurchaseExist
(
@PurchaseID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(purchase_id) 
FROM [purchase] 
WHERE purchase_id = @PurchaseID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spPurchaseExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spPurchaseExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spPurchaseLoad >>>>'
	DROP PROCEDURE [spPurchaseLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spPurchaseLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spPurchaseLoad
(
@PurchaseID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [purchase_id], [date_created], [date_modified], [authtoken], [is_success], [is_error], [error_trace], [response_json], [amount_in_pennies], [num_items_in_cart], [is_downloaded], [download_date] 
FROM [purchase] 
WHERE purchase_id = @PurchaseID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spPurchaseLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spPurchaseLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spPurchaseUpdate >>>>'
	DROP PROCEDURE [spPurchaseUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spPurchaseUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spPurchaseUpdate
(
	@PurchaseID                NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@Authtoken                 NVARCHAR(255) = NULL,
	@IsSuccess                 NUMERIC(1,0) = 0,
	@IsError                   NUMERIC(1,0) = 0,
	@ErrorTrace                TEXT = NULL,
	@ResponseJson              TEXT = NULL,
	@AmountInPennies           NUMERIC(10,0) = 0,
	@NumItemsInCart            NUMERIC(10,0) = 0,
	@IsDownloaded              NUMERIC(1,0) = 0,
	@DownloadDate              DATETIME = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [purchase] SET 
	[date_modified] = @DateModified,
	[authtoken] = @Authtoken,
	[is_success] = @IsSuccess,
	[is_error] = @IsError,
	[error_trace] = @ErrorTrace,
	[response_json] = @ResponseJson,
	[amount_in_pennies] = @AmountInPennies,
	[num_items_in_cart] = @NumItemsInCart,
	[is_downloaded] = @IsDownloaded,
	[download_date] = @DownloadDate
WHERE purchase_id = @PurchaseID

-- return ID for updated record
SELECT @PKID = @PurchaseID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spPurchaseUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spPurchaseUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spPurchaseDelete >>>>'
	DROP PROCEDURE [spPurchaseDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spPurchaseDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spPurchaseDelete
(
@PurchaseID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [purchase] 
WHERE purchase_id = @PurchaseID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spPurchaseDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spPurchaseDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spPurchaseInsert >>>>'
	DROP PROCEDURE [spPurchaseInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spPurchaseInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/21/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spPurchaseInsert
(
	@PurchaseID                NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Authtoken                 NVARCHAR(255) = NULL,
	@IsSuccess                 NUMERIC(1,0) = 0,
	@IsError                   NUMERIC(1,0) = 0,
	@ErrorTrace                TEXT = NULL,
	@ResponseJson              TEXT = NULL,
	@AmountInPennies           NUMERIC(10,0) = 0,
	@NumItemsInCart            NUMERIC(10,0) = 0,
	@IsDownloaded              NUMERIC(1,0) = 0,
	@DownloadDate              DATETIME = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [purchase]
(
	[date_created],
	[authtoken],
	[is_success],
	[is_error],
	[error_trace],
	[response_json],
	[amount_in_pennies],
	[num_items_in_cart],
	[is_downloaded],
	[download_date]
)
 VALUES 
(
	@DateCreated,
	@Authtoken,
	@IsSuccess,
	@IsError,
	@ErrorTrace,
	@ResponseJson,
	@AmountInPennies,
	@NumItemsInCart,
	@IsDownloaded,
	@DownloadDate
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spPurchaseInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spPurchaseInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spPurchaseEnum >>>>'
	DROP PROCEDURE [spPurchaseEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spPurchaseEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/21/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spPurchaseEnum
	@PurchaseID                NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Authtoken                 NVARCHAR(255) = NULL,
	@IsSuccess                 NUMERIC(1,0) = NULL,
	@IsError                   NUMERIC(1,0) = NULL,
	@ErrorTrace                TEXT = NULL,
	@ResponseJson              TEXT = NULL,
	@AmountInPennies           NUMERIC(10,0) = 0,
	@NumItemsInCart            NUMERIC(10,0) = 0,
	@IsDownloaded              NUMERIC(1,0) = NULL,
    	@BeginDownloadDate         DATETIME = NULL,
    	@EndDownloadDate           DATETIME = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [purchase_id], [date_created], [date_modified], [authtoken], [is_success], [is_error], [error_trace], [response_json], [amount_in_pennies], [num_items_in_cart], [is_downloaded], [download_date]
      FROM [purchase] 
      WHERE ((@PurchaseID = 0) OR ([purchase_id] LIKE @PurchaseID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@Authtoken IS NULL) OR ([authtoken] LIKE @Authtoken))
      AND ((@IsSuccess IS NULL) OR ([is_success] LIKE @IsSuccess))
      AND ((@IsError IS NULL) OR ([is_error] LIKE @IsError))
      AND ((@ErrorTrace IS NULL) OR ([error_trace] LIKE @ErrorTrace))
      AND ((@ResponseJson IS NULL) OR ([response_json] LIKE @ResponseJson))
      AND ((@AmountInPennies = 0) OR ([amount_in_pennies] LIKE @AmountInPennies))
      AND ((@NumItemsInCart = 0) OR ([num_items_in_cart] LIKE @NumItemsInCart))
      AND ((@IsDownloaded IS NULL) OR ([is_downloaded] LIKE @IsDownloaded))
      AND ((@BeginDownloadDate IS NULL) OR ([download_date] >= @BeginDownloadDate))
      AND ((@EndDownloadDate IS NULL) OR ([download_date] <= @EndDownloadDate))
 ORDER BY [purchase_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spPurchaseEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spPurchaseEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spPurchaseEnum >>>'
GO

