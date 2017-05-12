IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserEnum1 >>>>'
	DROP PROCEDURE [spUserEnum1]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserEnum1
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		5/9/2017		HA		Created
*******************************************************************************/




CREATE PROCEDURE spUserEnum1
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
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [has_current_rating], [current_rating], [internal_calculated_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [is_rating_profile_finished], [user_source_id]
      FROM [user] 
      WHERE ([user_role_id] = @UserRoleID) 
	  AND (([fullname] LIKE @Fullname) 
	  OR ([firstname] LIKE @Firstname)
	  OR ([lastname] LIKE @Lastname)
	  OR ([phone_number] LIKE @PhoneNumber)
	  OR ([username] LIKE @Username))
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum1 >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum1 >>>'
GO



IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	PRINT '<<<< Dropping Stored Procedure spUserEnum2 >>>>'
	DROP PROCEDURE [spUserEnum2]
END
GO

/*******************************************************************************
**		PROCEDURE NAME: spUserEnum2
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		5/9/2017		HA		Created
*******************************************************************************/

CREATE PROCEDURE spUserEnum2
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
 	@COUNT                    NUMERIC(10,0) = 0 OUTPUT

AS
    	SET NOCOUNT ON


      SELECT  [user_id], [user_role_id], [date_created], [date_modified], [fullname], [firstname], [middlename], [lastname], [phone_number], [username], [passwd], [ssn], [picture_url], [picture], [is_disabled], [welcome_email_sent], [validationtoken], [validationlink], [isvalidated], [welcome_email_sent_date], [last_login_date], [internal_notes], [user_message], [cookie_id], [has_current_rating], [current_rating], [internal_calculated_rating], [security_question], [security_answer], [number_of_visits], [previous_visit_date], [last_visit_date], [is_rating_profile_finished], [user_source_id]
      FROM [user] 
      WHERE ([user_role_id] != 4) 
	  AND (([fullname] LIKE @Fullname) 
	  OR ([firstname] LIKE @Firstname)
	  OR ([lastname] LIKE @Lastname)
	  OR ([phone_number] LIKE @PhoneNumber)
	  OR ([username] LIKE @Username))
 ORDER BY [user_id] ASC


      SELECT @COUNT=@@rowcount 

    	RETURN 0

GO
IF EXISTS (select * from sysobjects where id = object_id(N'[spUserEnum2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
PRINT '<<<< Created Stored Procedure spUserEnum2 >>>>'
ELSE
PRINT '<<< Failed Creating Stored Procedure spUserEnum2 >>>'
GO