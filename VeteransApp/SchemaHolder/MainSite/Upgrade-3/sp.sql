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
**		3/23/2017		HA		Created
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
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserLoad
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [has_current_rating], [current_rating], [internal_calculated_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [is_rating_profile_finished] 
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
**		3/23/2017		HA		Created
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
	@HasCurrentRating          NUMERIC(1,0) = 0,
	@CurrentRating             NUMERIC(10,0) = 0,
	@InternalCalculatedRating  NUMERIC(10,0) = 0,
	@SecurityQuestion          NVARCHAR(255) = NULL,
	@SecurityAnswer            NVARCHAR(255) = NULL,
	@NumberOfVisits            NUMERIC(10,0) = 0,
	@PreviousVisitDate         DATETIME = NULL,
	@LastVisitDate             DATETIME = NULL,
	@IsRatingProfileFinished   NUMERIC(1,0) = 0,
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
	[has_current_rating] = @HasCurrentRating,
	[current_rating] = @CurrentRating,
	[internal_calculated_rating] = @InternalCalculatedRating,
	[security_question] = @SecurityQuestion,
	[security_answer] = @SecurityAnswer,
	[number_of_visits] = @NumberOfVisits,
	[previous_visit_date] = @PreviousVisitDate,
	[last_visit_date] = @LastVisitDate,
	[is_rating_profile_finished] = @IsRatingProfileFinished
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
**		3/23/2017		HA		Created
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
**		3/23/2017		HA		Created
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
	@HasCurrentRating          NUMERIC(1,0) = 0,
	@CurrentRating             NUMERIC(10,0) = 0,
	@InternalCalculatedRating  NUMERIC(10,0) = 0,
	@SecurityQuestion          NVARCHAR(255) = NULL,
	@SecurityAnswer            NVARCHAR(255) = NULL,
	@NumberOfVisits            NUMERIC(10,0) = 0,
	@PreviousVisitDate         DATETIME = NULL,
	@LastVisitDate             DATETIME = NULL,
	@IsRatingProfileFinished   NUMERIC(1,0) = 0,
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
	[has_current_rating],
	[current_rating],
	[internal_calculated_rating],
	[security_question],
	[security_answer],
	[number_of_visits],
	[previous_visit_date],
	[last_visit_date],
	[is_rating_profile_finished]
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
	@HasCurrentRating,
	@CurrentRating,
	@InternalCalculatedRating,
	@SecurityQuestion,
	@SecurityAnswer,
	@NumberOfVisits,
	@PreviousVisitDate,
	@LastVisitDate,
	@IsRatingProfileFinished
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
**		3/23/2017		HA		Created
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
	@HasCurrentRating          NUMERIC(1,0) = NULL,
	@CurrentRating             NUMERIC(10,0) = 0,
	@InternalCalculatedRating  NUMERIC(10,0) = 0,
	@SecurityQuestion          NVARCHAR(255) = NULL,
	@SecurityAnswer            NVARCHAR(255) = NULL,
	@NumberOfVisits            NUMERIC(10,0) = 0,
    	@BeginPreviousVisitDate    DATETIME = NULL,
    	@EndPreviousVisitDate      DATETIME = NULL,
    	@BeginLastVisitDate        DATETIME = NULL,
    	@EndLastVisitDate          DATETIME = NULL,
	@IsRatingProfileFinished   NUMERIC(1,0) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [has_current_rating], [current_rating], [internal_calculated_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [is_rating_profile_finished]
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
      AND ((@HasCurrentRating IS NULL) OR ([has_current_rating] LIKE @HasCurrentRating))
      AND ((@CurrentRating = 0) OR ([current_rating] LIKE @CurrentRating))
      AND ((@InternalCalculatedRating = 0) OR ([internal_calculated_rating] LIKE @InternalCalculatedRating))
      AND ((@SecurityQuestion IS NULL) OR ([security_question] LIKE @SecurityQuestion))
      AND ((@SecurityAnswer IS NULL) OR ([security_answer] LIKE @SecurityAnswer))
      AND ((@NumberOfVisits = 0) OR ([number_of_visits] LIKE @NumberOfVisits))
      AND ((@BeginPreviousVisitDate IS NULL) OR ([previous_visit_date] >= @BeginPreviousVisitDate))
      AND ((@EndPreviousVisitDate IS NULL) OR ([previous_visit_date] <= @EndPreviousVisitDate))
      AND ((@BeginLastVisitDate IS NULL) OR ([last_visit_date] >= @BeginLastVisitDate))
      AND ((@EndLastVisitDate IS NULL) OR ([last_visit_date] <= @EndLastVisitDate))
      AND ((@IsRatingProfileFinished IS NULL) OR ([is_rating_profile_finished] LIKE @IsRatingProfileFinished))
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum >>>'
GO



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
**		3/23/2017		HA		Created
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
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeLoad
(
@JctUserContentTypeID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [jct_user_content_type_id], [date_created], [date_modified], [user_id], [side_id], [content_type_id], [rating] 
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
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeUpdate
(
	@JctUserContentTypeID      NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@UserID                    NUMERIC(10) = 0,
	@SideID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@Rating                    NUMERIC(10,0) = 0,
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
	[rating] = @Rating
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
**		3/23/2017		HA		Created
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
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spJctUserContentTypeInsert
(
	@JctUserContentTypeID      NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@UserID                    NUMERIC(10) = 0,
	@SideID                    NUMERIC(10) = 0,
	@ContentTypeID             NUMERIC(10) = 0,
	@Rating                    NUMERIC(10,0) = 0,
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
	[rating]
)
 VALUES 
(
	@DateCreated,
	@UserID,
	@SideID,
	@ContentTypeID,
	@Rating
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
**		3/23/2017		HA		Created
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
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [jct_user_content_type_id], [date_created], [date_modified], [user_id], [side_id], [content_type_id], [rating]
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
 ORDER BY [jct_user_content_type_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spJctUserContentTypeEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spJctUserContentTypeEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spJctUserContentTypeEnum >>>'
GO


IF EXISTS (select * from sysobjects where id = object_id(N'[spSideExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSideExist >>>>'
	DROP PROCEDURE [spSideExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSideExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSideExist
(
@SideID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(side_id) 
FROM [side] 
WHERE side_id = @SideID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSideExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSideExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSideLoad >>>>'
	DROP PROCEDURE [spSideLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSideLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSideLoad
(
@SideID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [side_id], [date_created], [code], [description], [visible_code] 
FROM [side] 
WHERE side_id = @SideID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSideLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSideLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSideUpdate >>>>'
	DROP PROCEDURE [spSideUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSideUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSideUpdate
(
	@SideID                    NUMERIC(10) = 0,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [side] SET 
	[code] = @Code,
	[description] = @Description,
	[visible_code] = @VisibleCode
WHERE side_id = @SideID

-- return ID for updated record
SELECT @PKID = @SideID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSideUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSideUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSideDelete >>>>'
	DROP PROCEDURE [spSideDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSideDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSideDelete
(
@SideID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [side] 
WHERE side_id = @SideID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSideDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSideDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSideInsert >>>>'
	DROP PROCEDURE [spSideInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSideInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/23/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSideInsert
(
	@SideID                    NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [side]
(
	[side_id],
	[date_created],
	[code],
	[description],
	[visible_code]
)
 VALUES 
(
	@SideID,
	@DateCreated,
	@Code,
	@Description,
	@VisibleCode
)


-- return ID for new record
SELECT @PKID = @SideID

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSideInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSideInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSideEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSideEnum >>>>'
	DROP PROCEDURE [spSideEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSideEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/23/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spSideEnum
	@SideID                    NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [side_id], [date_created], [code], [description], [visible_code]
      FROM [side] 
      WHERE ((@SideID = 0) OR ([side_id] LIKE @SideID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Code IS NULL) OR ([code] LIKE @Code))
      AND ((@Description IS NULL) OR ([description] LIKE @Description))
      AND ((@VisibleCode IS NULL) OR ([visible_code] LIKE @VisibleCode))
 ORDER BY [side_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spSideEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSideEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSideEnum >>>'
GO

