IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spDbversionExist >>>>'
	DROP PROCEDURE [spDbversionExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spDbversionExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spDbversionExist
(
@DbversionID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(dbversion_id) 
FROM [dbversion] 
WHERE dbversion_id = @DbversionID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spDbversionExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spDbversionExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spDbversionLoad >>>>'
	DROP PROCEDURE [spDbversionLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spDbversionLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spDbversionLoad
(
@DbversionID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [dbversion_id], [date_created], [major_num], [minor_num], [notes] 
FROM [dbversion] 
WHERE dbversion_id = @DbversionID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spDbversionLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spDbversionLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spDbversionUpdate >>>>'
	DROP PROCEDURE [spDbversionUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spDbversionUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spDbversionUpdate
(
	@DbversionID               NUMERIC(10) = 0,
	@MajorNum                  NUMERIC(10,0) = 0,
	@MinorNum                  NUMERIC(10,0) = 0,
	@Notes                     TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [dbversion] SET 
	[major_num] = @MajorNum,
	[minor_num] = @MinorNum,
	[notes] = @Notes
WHERE dbversion_id = @DbversionID

-- return ID for updated record
SELECT @PKID = @DbversionID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spDbversionUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spDbversionUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spDbversionDelete >>>>'
	DROP PROCEDURE [spDbversionDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spDbversionDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spDbversionDelete
(
@DbversionID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [dbversion] 
WHERE dbversion_id = @DbversionID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spDbversionDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spDbversionDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spDbversionInsert >>>>'
	DROP PROCEDURE [spDbversionInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spDbversionInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spDbversionInsert
(
	@DbversionID               NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@MajorNum                  NUMERIC(10,0) = 0,
	@MinorNum                  NUMERIC(10,0) = 0,
	@Notes                     TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [dbversion]
(
	[dbversion_id],
	[date_created],
	[major_num],
	[minor_num],
	[notes]
)
 VALUES 
(
	@DbversionID,
	@DateCreated,
	@MajorNum,
	@MinorNum,
	@Notes
)


-- return ID for new record
SELECT @PKID = @DbversionID

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spDbversionInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spDbversionInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spDbversionEnum >>>>'
	DROP PROCEDURE [spDbversionEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spDbversionEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spDbversionEnum
	@DbversionID               NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@MajorNum                  NUMERIC(10,0) = 0,
	@MinorNum                  NUMERIC(10,0) = 0,
	@Notes                     TEXT = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [dbversion_id], [date_created], [major_num], [minor_num], [notes]
      FROM [dbversion] 
      WHERE ((@DbversionID = 0) OR ([dbversion_id] LIKE @DbversionID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@MajorNum = 0) OR ([major_num] LIKE @MajorNum))
      AND ((@MinorNum = 0) OR ([minor_num] LIKE @MinorNum))
      AND ((@Notes IS NULL) OR ([notes] LIKE @Notes))
 ORDER BY [dbversion_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spDbversionEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spDbversionEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spDbversionEnum >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spEvaluationExist >>>>'
	DROP PROCEDURE [spEvaluationExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spEvaluationExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spEvaluationExist
(
@EvaluationID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(evaluation_id) 
FROM [evaluation] 
WHERE evaluation_id = @EvaluationID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spEvaluationExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spEvaluationExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spEvaluationLoad >>>>'
	DROP PROCEDURE [spEvaluationLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spEvaluationLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spEvaluationLoad
(
@EvaluationID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [evaluation_id], [user_id], [date_created], [date_modified], [is_firsttime_filing], [has_a_claim], [has_active_appeal], [current_rating] 
FROM [evaluation] 
WHERE evaluation_id = @EvaluationID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spEvaluationLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spEvaluationLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spEvaluationUpdate >>>>'
	DROP PROCEDURE [spEvaluationUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spEvaluationUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spEvaluationUpdate
(
	@EvaluationID              NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@IsFirsttimeFiling         NUMERIC(1,0) = 0,
	@HasAClaim                 NUMERIC(1,0) = 0,
	@HasActiveAppeal           NUMERIC(1,0) = 0,
	@CurrentRating             NUMERIC(10,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [evaluation] SET 
	[user_id] = @UserID,
	[date_modified] = @DateModified,
	[is_firsttime_filing] = @IsFirsttimeFiling,
	[has_a_claim] = @HasAClaim,
	[has_active_appeal] = @HasActiveAppeal,
	[current_rating] = @CurrentRating
WHERE evaluation_id = @EvaluationID

-- return ID for updated record
SELECT @PKID = @EvaluationID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spEvaluationUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spEvaluationUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spEvaluationDelete >>>>'
	DROP PROCEDURE [spEvaluationDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spEvaluationDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spEvaluationDelete
(
@EvaluationID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [evaluation] 
WHERE evaluation_id = @EvaluationID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spEvaluationDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spEvaluationDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spEvaluationInsert >>>>'
	DROP PROCEDURE [spEvaluationInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spEvaluationInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spEvaluationInsert
(
	@EvaluationID              NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@IsFirsttimeFiling         NUMERIC(1,0) = 0,
	@HasAClaim                 NUMERIC(1,0) = 0,
	@HasActiveAppeal           NUMERIC(1,0) = 0,
	@CurrentRating             NUMERIC(10,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [evaluation]
(
	[user_id],
	[date_created],
	[is_firsttime_filing],
	[has_a_claim],
	[has_active_appeal],
	[current_rating]
)
 VALUES 
(
	@UserID,
	@DateCreated,
	@IsFirsttimeFiling,
	@HasAClaim,
	@HasActiveAppeal,
	@CurrentRating
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spEvaluationInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spEvaluationInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spEvaluationEnum >>>>'
	DROP PROCEDURE [spEvaluationEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spEvaluationEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spEvaluationEnum
	@EvaluationID              NUMERIC(10) = 0,
	@UserID                    NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@IsFirsttimeFiling         NUMERIC(1,0) = NULL,
	@HasAClaim                 NUMERIC(1,0) = NULL,
	@HasActiveAppeal           NUMERIC(1,0) = NULL,
	@CurrentRating             NUMERIC(10,0) = 0,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [evaluation_id], [user_id], [date_created], [date_modified], [is_firsttime_filing], [has_a_claim], [has_active_appeal], [current_rating]
      FROM [evaluation] 
      WHERE ((@EvaluationID = 0) OR ([evaluation_id] LIKE @EvaluationID))
      AND ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@IsFirsttimeFiling IS NULL) OR ([is_firsttime_filing] LIKE @IsFirsttimeFiling))
      AND ((@HasAClaim IS NULL) OR ([has_a_claim] LIKE @HasAClaim))
      AND ((@HasActiveAppeal IS NULL) OR ([has_active_appeal] LIKE @HasActiveAppeal))
      AND ((@CurrentRating = 0) OR ([current_rating] LIKE @CurrentRating))
 ORDER BY [evaluation_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spEvaluationEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spEvaluationEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spEvaluationEnum >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSyslogExist >>>>'
	DROP PROCEDURE [spSyslogExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSyslogExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSyslogExist
(
@SyslogID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(syslog_id) 
FROM [syslog] 
WHERE syslog_id = @SyslogID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSyslogExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSyslogExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSyslogLoad >>>>'
	DROP PROCEDURE [spSyslogLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSyslogLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSyslogLoad
(
@SyslogID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [syslog_id], [interaction_id], [date_created], [date_modified], [msgsource], [msgaction], [msgtxt] 
FROM [syslog] 
WHERE syslog_id = @SyslogID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSyslogLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSyslogLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSyslogUpdate >>>>'
	DROP PROCEDURE [spSyslogUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSyslogUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSyslogUpdate
(
	@SyslogID                  NUMERIC(10) = 0,
	@InteractionID             NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@Msgsource                 NVARCHAR(255) = NULL,
	@Msgaction                 NVARCHAR(255) = NULL,
	@Msgtxt                    TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [syslog] SET 
	[interaction_id] = @InteractionID,
	[date_modified] = @DateModified,
	[msgsource] = @Msgsource,
	[msgaction] = @Msgaction,
	[msgtxt] = @Msgtxt
WHERE syslog_id = @SyslogID

-- return ID for updated record
SELECT @PKID = @SyslogID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSyslogUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSyslogUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSyslogDelete >>>>'
	DROP PROCEDURE [spSyslogDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSyslogDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSyslogDelete
(
@SyslogID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [syslog] 
WHERE syslog_id = @SyslogID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSyslogDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSyslogDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSyslogInsert >>>>'
	DROP PROCEDURE [spSyslogInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSyslogInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spSyslogInsert
(
	@SyslogID                  NUMERIC(10) = 0,
	@InteractionID             NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Msgsource                 NVARCHAR(255) = NULL,
	@Msgaction                 NVARCHAR(255) = NULL,
	@Msgtxt                    TEXT = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [syslog]
(
	[interaction_id],
	[date_created],
	[msgsource],
	[msgaction],
	[msgtxt]
)
 VALUES 
(
	@InteractionID,
	@DateCreated,
	@Msgsource,
	@Msgaction,
	@Msgtxt
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSyslogInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSyslogInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spSyslogEnum >>>>'
	DROP PROCEDURE [spSyslogEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spSyslogEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spSyslogEnum
	@SyslogID                  NUMERIC(10) = 0,
	@InteractionID             NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Msgsource                 NVARCHAR(255) = NULL,
	@Msgaction                 NVARCHAR(255) = NULL,
	@Msgtxt                    TEXT = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [syslog_id], [interaction_id], [date_created], [date_modified], [msgsource], [msgaction], [msgtxt]
      FROM [syslog] 
      WHERE ((@SyslogID = 0) OR ([syslog_id] LIKE @SyslogID))
      AND ((@InteractionID = 0) OR ([interaction_id] LIKE @InteractionID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@Msgsource IS NULL) OR ([msgsource] LIKE @Msgsource))
      AND ((@Msgaction IS NULL) OR ([msgaction] LIKE @Msgaction))
      AND ((@Msgtxt IS NULL) OR ([msgtxt] LIKE @Msgtxt))
 ORDER BY [syslog_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spSyslogEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spSyslogEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spSyslogEnum >>>'
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
**		2/27/2017		HA		Created
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
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserLoad
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message] 
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
**		2/27/2017		HA		Created
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
	@InternalNotes             NVARCHAR(255) = NULL,
	@UserMessage               NVARCHAR(255) = NULL,
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
	[user_message] = @UserMessage
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
**		2/27/2017		HA		Created
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
**		2/27/2017		HA		Created
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
	@InternalNotes             NVARCHAR(255) = NULL,
	@UserMessage               NVARCHAR(255) = NULL,
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
	[user_message]
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
	@UserMessage
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
**		2/27/2017		HA		Created
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
	@InternalNotes             NVARCHAR(255) = NULL,
	@UserMessage               NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message]
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
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserRoleExist >>>>'
	DROP PROCEDURE [spUserRoleExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserRoleExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserRoleExist
(
@UserRoleID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(user_role_id) 
FROM [user_role] 
WHERE user_role_id = @UserRoleID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserRoleExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserRoleExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserRoleLoad >>>>'
	DROP PROCEDURE [spUserRoleLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserRoleLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserRoleLoad
(
@UserRoleID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_role_id], [date_created], [code], [description], [visible_code] 
FROM [user_role] 
WHERE user_role_id = @UserRoleID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserRoleLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserRoleLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserRoleUpdate >>>>'
	DROP PROCEDURE [spUserRoleUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserRoleUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserRoleUpdate
(
	@UserRoleID                NUMERIC(10) = 0,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [user_role] SET 
	[code] = @Code,
	[description] = @Description,
	[visible_code] = @VisibleCode
WHERE user_role_id = @UserRoleID

-- return ID for updated record
SELECT @PKID = @UserRoleID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserRoleUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserRoleUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserRoleDelete >>>>'
	DROP PROCEDURE [spUserRoleDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserRoleDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserRoleDelete
(
@UserRoleID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [user_role] 
WHERE user_role_id = @UserRoleID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserRoleDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserRoleDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserRoleInsert >>>>'
	DROP PROCEDURE [spUserRoleInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserRoleInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserRoleInsert
(
	@UserRoleID                NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [user_role]
(
	[user_role_id],
	[date_created],
	[code],
	[description],
	[visible_code]
)
 VALUES 
(
	@UserRoleID,
	@DateCreated,
	@Code,
	@Description,
	@VisibleCode
)


-- return ID for new record
SELECT @PKID = @UserRoleID

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserRoleInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserRoleInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserRoleEnum >>>>'
	DROP PROCEDURE [spUserRoleEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserRoleEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/27/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spUserRoleEnum
	@UserRoleID                NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@Code                      NVARCHAR(255) = NULL,
	@Description               NVARCHAR(255) = NULL,
	@VisibleCode               NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_role_id], [date_created], [code], [description], [visible_code]
      FROM [user_role] 
      WHERE ((@UserRoleID = 0) OR ([user_role_id] LIKE @UserRoleID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Code IS NULL) OR ([code] LIKE @Code))
      AND ((@Description IS NULL) OR ([description] LIKE @Description))
      AND ((@VisibleCode IS NULL) OR ([visible_code] LIKE @VisibleCode))
 ORDER BY [user_role_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserRoleEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserRoleEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserRoleEnum >>>'
GO

