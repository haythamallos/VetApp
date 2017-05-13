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
**		5/13/2017		HA		Created
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
**		5/13/2017		HA		Created
*******************************************************************************/
CREATE PROCEDURE spUserLoad
(
@UserID        NUMERIC(10) = 0
)
AS
SET NOCOUNT ON

-- select record(s) with specified id

SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [has_current_rating], [current_rating], [internal_calculated_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [is_rating_profile_finished], [user_source_id], [impersonate] 
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
**		5/13/2017		HA		Created
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
	@UserSourceID              NUMERIC(10) = 0,
	@Impersonate               NUMERIC(1,0) = 0,
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
	[is_rating_profile_finished] = @IsRatingProfileFinished,
	[user_source_id] = @UserSourceID,
	[impersonate] = @Impersonate
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
**		5/13/2017		HA		Created
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
**		5/13/2017		HA		Created
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
	@UserSourceID              NUMERIC(10) = 0,
	@Impersonate               NUMERIC(1,0) = 0,
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
	[is_rating_profile_finished],
	[user_source_id],
	[impersonate]
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
	@IsRatingProfileFinished,
	@UserSourceID,
	@Impersonate
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
**		5/13/2017		HA		Created
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
	@UserSourceID              NUMERIC(10) = 0,
	@Impersonate               NUMERIC(1,0) = NULL,
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [has_current_rating], [current_rating], [internal_calculated_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [is_rating_profile_finished], [user_source_id], [impersonate]
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
      AND ((@UserSourceID = 0) OR ([user_source_id] LIKE @UserSourceID))
      AND ((@Impersonate IS NULL) OR ([impersonate] LIKE @Impersonate))
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum >>>'
GO

