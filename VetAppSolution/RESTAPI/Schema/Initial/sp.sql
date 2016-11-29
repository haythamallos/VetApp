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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApilogExist >>>>'
	DROP PROCEDURE [spApilogExist]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApilogExist
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApilogExist
(
@ApilogID        NUMERIC(10) = 0,
@COUNT          INT = 0 OUTPUT
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

SELECT @COUNT = COUNT(apilog_id) 
FROM [apilog] 
WHERE apilog_id = @ApilogID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogExist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApilogExist >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApilogExist >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApilogLoad >>>>'
	DROP PROCEDURE [spApilogLoad]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApilogLoad
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApilogLoad
(
@ApilogID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [apilog_id], [apikey_id], [ref_num], [date_created], [msgsource], [trace], [is_success], [in_progress], [http_status_str], [http_status_num], [msgtxt], [reqtxt], [resptxt], [duration_in_ms], [call_start_time], [call_end_time], [searchtext], [authuserid] 
FROM [apilog] 
WHERE apilog_id = @ApilogID
RETURN 0
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApilogLoad >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApilogLoad >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApilogUpdate >>>>'
	DROP PROCEDURE [spApilogUpdate]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApilogUpdate
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApilogUpdate
(
	@ApilogID                  NUMERIC(10) = 0,
	@ApikeyID                  NUMERIC(10) = 0,
	@RefNum                    NUMERIC(10,0) = 0,
	@Msgsource                 NVARCHAR(255) = NULL,
	@Trace                     NVARCHAR(255) = NULL,
	@IsSuccess                 NUMERIC(1,0) = 0,
	@InProgress                NUMERIC(1,0) = 0,
	@HttpStatusStr             NVARCHAR(255) = NULL,
	@HttpStatusNum             NUMERIC(10,0) = 0,
	@Msgtxt                    TEXT = NULL,
	@Reqtxt                    TEXT = NULL,
	@Resptxt                   TEXT = NULL,
	@DurationInMs              NUMERIC(10,0) = 0,
	@CallStartTime             DATETIME = NULL,
	@CallEndTime               DATETIME = NULL,
	@Searchtext                NVARCHAR(255) = NULL,
	@Authuserid                NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [apilog] SET 
	[apikey_id] = @ApikeyID,
	[ref_num] = @RefNum,
	[msgsource] = @Msgsource,
	[trace] = @Trace,
	[is_success] = @IsSuccess,
	[in_progress] = @InProgress,
	[http_status_str] = @HttpStatusStr,
	[http_status_num] = @HttpStatusNum,
	[msgtxt] = @Msgtxt,
	[reqtxt] = @Reqtxt,
	[resptxt] = @Resptxt,
	[duration_in_ms] = @DurationInMs,
	[call_start_time] = @CallStartTime,
	[call_end_time] = @CallEndTime,
	[searchtext] = @Searchtext,
	[authuserid] = @Authuserid
WHERE apilog_id = @ApilogID

-- return ID for updated record
SELECT @PKID = @ApilogID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApilogUpdate >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApilogUpdate >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApilogDelete >>>>'
	DROP PROCEDURE [spApilogDelete]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApilogDelete
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApilogDelete
(
@ApilogID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- check if a record with the specified id exists

DELETE FROM [apilog] 
WHERE apilog_id = @ApilogID
------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApilogDelete >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApilogDelete >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApilogInsert >>>>'
	DROP PROCEDURE [spApilogInsert]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApilogInsert
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spApilogInsert
(
	@ApilogID                  NUMERIC(10) = 0,
	@ApikeyID                  NUMERIC(10) = 0,
	@RefNum                    NUMERIC(10,0) = 0,
	@DateCreated               DATETIME = NULL,
	@Msgsource                 NVARCHAR(255) = NULL,
	@Trace                     NVARCHAR(255) = NULL,
	@IsSuccess                 NUMERIC(1,0) = 0,
	@InProgress                NUMERIC(1,0) = 0,
	@HttpStatusStr             NVARCHAR(255) = NULL,
	@HttpStatusNum             NUMERIC(10,0) = 0,
	@Msgtxt                    TEXT = NULL,
	@Reqtxt                    TEXT = NULL,
	@Resptxt                   TEXT = NULL,
	@DurationInMs              NUMERIC(10,0) = 0,
	@CallStartTime             DATETIME = NULL,
	@CallEndTime               DATETIME = NULL,
	@Searchtext                NVARCHAR(255) = NULL,
	@Authuserid                NVARCHAR(255) = NULL,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [apilog]
(
	[apikey_id],
	[ref_num],
	[date_created],
	[msgsource],
	[trace],
	[is_success],
	[in_progress],
	[http_status_str],
	[http_status_num],
	[msgtxt],
	[reqtxt],
	[resptxt],
	[duration_in_ms],
	[call_start_time],
	[call_end_time],
	[searchtext],
	[authuserid]
)
 VALUES 
(
	@ApikeyID,
	@RefNum,
	@DateCreated,
	@Msgsource,
	@Trace,
	@IsSuccess,
	@InProgress,
	@HttpStatusStr,
	@HttpStatusNum,
	@Msgtxt,
	@Reqtxt,
	@Resptxt,
	@DurationInMs,
	@CallStartTime,
	@CallEndTime,
	@Searchtext,
	@Authuserid
)


-- return ID for new record
SELECT @PKID = SCOPE_IDENTITY()

------------------------------------------------
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApilogInsert >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApilogInsert >>>'
GO

IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spApilogEnum >>>>'
	DROP PROCEDURE [spApilogEnum]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spApilogEnum
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/28/2016		HA		Created
*******************************************************************************/




CREATE PROCEDURE spApilogEnum
	@ApilogID                  NUMERIC(10) = 0,
	@ApikeyID                  NUMERIC(10) = 0,
	@RefNum                    NUMERIC(10,0) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
	@Msgsource                 NVARCHAR(255) = NULL,
	@Trace                     NVARCHAR(255) = NULL,
	@IsSuccess                 NUMERIC(1,0) = NULL,
	@InProgress                NUMERIC(1,0) = NULL,
	@HttpStatusStr             NVARCHAR(255) = NULL,
	@HttpStatusNum             NUMERIC(10,0) = 0,
	@Msgtxt                    TEXT = NULL,
	@Reqtxt                    TEXT = NULL,
	@Resptxt                   TEXT = NULL,
	@DurationInMs              NUMERIC(10,0) = 0,
    	@BeginCallStartTime        DATETIME = NULL,
    	@EndCallStartTime          DATETIME = NULL,
    	@BeginCallEndTime          DATETIME = NULL,
    	@EndCallEndTime            DATETIME = NULL,
	@Searchtext                NVARCHAR(255) = NULL,
	@Authuserid                NVARCHAR(255) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [apilog_id], [apikey_id], [ref_num], [date_created], [msgsource], [trace], [is_success], [in_progress], [http_status_str], [http_status_num], [msgtxt], [reqtxt], [resptxt], [duration_in_ms], [call_start_time], [call_end_time], [searchtext], [authuserid]
      FROM [apilog] 
      WHERE ((@ApilogID = 0) OR ([apilog_id] LIKE @ApilogID))
      AND ((@ApikeyID = 0) OR ([apikey_id] LIKE @ApikeyID))
      AND ((@RefNum = 0) OR ([ref_num] LIKE @RefNum))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@Msgsource IS NULL) OR ([msgsource] LIKE @Msgsource))
      AND ((@Trace IS NULL) OR ([trace] LIKE @Trace))
      AND ((@IsSuccess IS NULL) OR ([is_success] LIKE @IsSuccess))
      AND ((@InProgress IS NULL) OR ([in_progress] LIKE @InProgress))
      AND ((@HttpStatusStr IS NULL) OR ([http_status_str] LIKE @HttpStatusStr))
      AND ((@HttpStatusNum = 0) OR ([http_status_num] LIKE @HttpStatusNum))
      AND ((@Msgtxt IS NULL) OR ([msgtxt] LIKE @Msgtxt))
      AND ((@Reqtxt IS NULL) OR ([reqtxt] LIKE @Reqtxt))
      AND ((@Resptxt IS NULL) OR ([resptxt] LIKE @Resptxt))
      AND ((@DurationInMs = 0) OR ([duration_in_ms] LIKE @DurationInMs))
      AND ((@BeginCallStartTime IS NULL) OR ([call_start_time] >= @BeginCallStartTime))
      AND ((@EndCallStartTime IS NULL) OR ([call_start_time] <= @EndCallStartTime))
      AND ((@BeginCallEndTime IS NULL) OR ([call_end_time] >= @BeginCallEndTime))
      AND ((@EndCallEndTime IS NULL) OR ([call_end_time] <= @EndCallEndTime))
      AND ((@Searchtext IS NULL) OR ([searchtext] LIKE @Searchtext))
      AND ((@Authuserid IS NULL) OR ([authuserid] LIKE @Authuserid))
 ORDER BY [apilog_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spApilogEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spApilogEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spApilogEnum >>>'
GO

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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserLoad
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_id], [user_role_id], [date_created], [date_modified], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [picture_url], [picture], [is_disabled] 
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
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserUpdate
(
	@UserID                    NUMERIC(10) = 0,
	@UserRoleID                NUMERIC(10) = 0,
	@DateModified              DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@Username                  NVARCHAR(255) = NULL,
	@Passwd                    NVARCHAR(255) = NULL,
	@PictureUrl                NVARCHAR(255) = NULL,
	@Picture                   VARBINARY(MAX) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

UPDATE [user] SET 
	[user_role_id] = @UserRoleID,
	[date_modified] = @DateModified,
	[firstname] = @Firstname,
	[middlename] = @Middlename,
	[lastname] = @Lastname,
	[phone_number] = @PhoneNumber,
	[username] = @Username,
	[passwd] = @Passwd,
	[picture_url] = @PictureUrl,
	[picture] = @Picture,
	[is_disabled] = @IsDisabled
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserInsert
(
	@UserID                    NUMERIC(10) = 0,
	@UserRoleID                NUMERIC(10) = 0,
	@DateCreated               DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@Username                  NVARCHAR(255) = NULL,
	@Passwd                    NVARCHAR(255) = NULL,
	@PictureUrl                NVARCHAR(255) = NULL,
	@Picture                   VARBINARY(MAX) = NULL,
	@IsDisabled                NUMERIC(1,0) = 0,
	@PKID                      NUMERIC(10) OUTPUT
)
AS
SET NOCOUNT ON

   -- Update record wth NUMERIC(10) value

INSERT INTO [user]
(
	[user_role_id],
	[date_created],
	[firstname],
	[middlename],
	[lastname],
	[phone_number],
	[username],
	[passwd],
	[picture_url],
	[picture],
	[is_disabled]
)
 VALUES 
(
	@UserRoleID,
	@DateCreated,
	@Firstname,
	@Middlename,
	@Lastname,
	@PhoneNumber,
	@Username,
	@Passwd,
	@PictureUrl,
	@Picture,
	@IsDisabled
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
**		11/28/2016		HA		Created
*******************************************************************************/




CREATE PROCEDURE spUserEnum
	@UserID                    NUMERIC(10) = 0,
	@UserRoleID                NUMERIC(10) = 0,
    	@BeginDateCreated          DATETIME = NULL,
    	@EndDateCreated            DATETIME = NULL,
    	@BeginDateModified         DATETIME = NULL,
    	@EndDateModified           DATETIME = NULL,
	@Firstname                 NVARCHAR(255) = NULL,
	@Middlename                NVARCHAR(255) = NULL,
	@Lastname                  NVARCHAR(255) = NULL,
	@PhoneNumber               NVARCHAR(255) = NULL,
	@Username                  NVARCHAR(255) = NULL,
	@Passwd                    NVARCHAR(255) = NULL,
	@PictureUrl                NVARCHAR(255) = NULL,
	@Picture                   VARBINARY(MAX) = NULL,
	@IsDisabled                NUMERIC(1,0) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [picture_url], [picture], [is_disabled]
      FROM [user] 
      WHERE ((@UserID = 0) OR ([user_id] LIKE @UserID))
      AND ((@UserRoleID = 0) OR ([user_role_id] LIKE @UserRoleID))
      AND ((@BeginDateCreated IS NULL) OR ([date_created] >= @BeginDateCreated))
      AND ((@EndDateCreated IS NULL) OR ([date_created] <= @EndDateCreated))
      AND ((@BeginDateModified IS NULL) OR ([date_modified] >= @BeginDateModified))
      AND ((@EndDateModified IS NULL) OR ([date_modified] <= @EndDateModified))
      AND ((@Firstname IS NULL) OR ([firstname] LIKE @Firstname))
      AND ((@Middlename IS NULL) OR ([middlename] LIKE @Middlename))
      AND ((@Lastname IS NULL) OR ([lastname] LIKE @Lastname))
      AND ((@PhoneNumber IS NULL) OR ([phone_number] LIKE @PhoneNumber))
      AND ((@Username IS NULL) OR ([username] LIKE @Username))
      AND ((@Passwd IS NULL) OR ([passwd] LIKE @Passwd))
      AND ((@PictureUrl IS NULL) OR ([picture_url] LIKE @PictureUrl))
      AND ((@Picture IS NULL) OR ([picture] LIKE @Picture))
      AND ((@IsDisabled IS NULL) OR ([is_disabled] LIKE @IsDisabled))
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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
**		11/28/2016		HA		Created
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

