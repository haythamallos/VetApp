/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/20/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'content_type')
  BEGIN
      PRINT 'Dropping Table content_type'

      DROP TABLE content_type
  END

go

CREATE TABLE content_type
  (
     content_type_id NUMERIC(10) NOT NULL PRIMARY KEY,
     date_created    DATETIME NULL,
     code            NVARCHAR(255) NULL,
     [description]     NVARCHAR(255) NULL,
     visible_code    NVARCHAR(255) NULL,
	 max_rating      INT NULL,
	 has_sides                   BIT NULL,
	 product_ref_name NVARCHAR(255) NULL,
	 product_ref_description NVARCHAR(255) NULL,
	 number_of_pages INT DEFAULT 0,
	 price_in_pennies INT NULL
	)

go

IF Object_id('content_type') IS NOT NULL
  PRINT '<<< CREATED TABLE content_type >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE content_type >>>'
go


/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/16/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'cart_item')
  BEGIN
      PRINT 'Dropping Table cart_item'

      DROP TABLE cart_item
  END

go

CREATE TABLE cart_item
  (
     cart_item_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
	 purchase_id [numeric](10, 0) NULL,
	 [user_id] [numeric](10, 0) NULL,
	 content_id [numeric](10, 0) NULL,
	 content_type_id [numeric](10, 0) NULL
  )

go

IF Object_id('cart_item') IS NOT NULL
  PRINT '<<< CREATED TABLE cart_item >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE cart_item >>>'

go

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		9/7/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'user')
  BEGIN
      PRINT 'Dropping Table user'

      DROP TABLE [user]
  END

go

CREATE TABLE [user]
  (
     user_id        NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
	 user_role_id [numeric](10, 0) NULL,
     date_created      DATETIME NULL,
     date_modified     DATETIME NULL,
     fullname        NVARCHAR(255) NULL,
     firstname        NVARCHAR(255) NULL,
     middlename        NVARCHAR(255) NULL,
     lastname        NVARCHAR(255) NULL,
     phone_number        NVARCHAR(255) NULL,
	 username        NVARCHAR(255) NULL,
	 passwd        NVARCHAR(255) NULL,
	 ssn        NVARCHAR(255) NULL,
     picture_url        NVARCHAR(255) NULL,
	 picture           VARBINARY(max) NULL,
	 is_disabled                   BIT NULL,
	 welcome_email_sent                   BIT NULL,
	 validationtoken  NVARCHAR(255) NULL,
     validationlink  NVARCHAR(255) NULL,
     isvalidated  BIT NULL,
     welcome_email_sent_date  DATETIME NULL,
     last_login_date  DATETIME NULL,
	 internal_notes        text NULL,
	 user_message        text NULL,
	 cookie_id  NVARCHAR(255) NULL,
	 has_current_rating  BIT NULL,
	 current_rating	INT NULL,
	 internal_calculated_rating  INT NULL,
	 security_question  NVARCHAR(255) NULL,
     security_answer  NVARCHAR(255) NULL,
	 number_of_visits	int default 0,
	 previous_visit_date     DATETIME NULL,
	 last_visit_date     DATETIME NULL,
	 is_rating_profile_finished   BIT NULL
 )

go

IF Object_id('user') IS NOT NULL
  PRINT '<<< CREATED TABLE user >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE user >>>'

go


/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/20/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'side')
  BEGIN
      PRINT 'Dropping Table side'

      DROP TABLE side
  END

go

CREATE TABLE side
  (
     side_id NUMERIC(10) NOT NULL PRIMARY KEY,
     date_created    DATETIME NULL,
     code            NVARCHAR(255) NULL,
     [description]     NVARCHAR(255) NULL,
	 visible_code    NVARCHAR(255) NULL
	)

go

IF Object_id('side') IS NOT NULL
  PRINT '<<< CREATED TABLE side >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE side >>>'
go


/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		3/16/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'jct_user_content_type')
  BEGIN
      PRINT 'Dropping Table jct_user_content_type'

      DROP TABLE jct_user_content_type
  END

go

CREATE TABLE jct_user_content_type
  (
     jct_user_content_type_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
	 [user_id] [numeric](10, 0) NULL,
	 [side_id] [numeric](10, 0) NULL,
	 content_type_id [numeric](10, 0) NULL,
	 rating	INT NULL
  )

go

IF Object_id('jct_user_content_type') IS NOT NULL
  PRINT '<<< CREATED TABLE jct_user_content_type >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE jct_user_content_type >>>'

go


alter table content add authtoken NVARCHAR(255) NULL
go

alter table content add error_purchase_id NUMERIC(10) NULL
go

alter table purchase add response_json TEXT NULL
go

alter table purchase add amount_in_pennies INT NULL
go

alter table purchase add num_items_in_cart INT NULL
go

alter table purchase add is_downloaded  BIT NULL
go

alter table purchase add download_date  DATETIME NULL
go

delete from content_type
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (1, GETDATE(), 'BACK', 'Back', 'Back', 40, 0, 9999, 'OMB Approved No. 2900-0808', 'BACK (THORACOLUMBAR SPINE) CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 11)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (2, GETDATE(), 'SHOULDER', 'Shoulder', 'Shoulder', 30, 1, 19999, 'OMB Approved No. 2900-0802', 'SHOULDER AND ARM CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 9)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (3, GETDATE(), 'NECK', 'Neck', 'Neck', 30, 0, 29999, 'OMB Approved No. 2900-0807', 'NECK (CERVICAL SPINE) CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 11)

INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (8, GETDATE(), 'DOWNLOADED', 'Content state downloaded', 'Content state downloaded')
go

delete from side
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'NONE', 'None', 'None')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'LEFT', 'Left', 'Left')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'RIGHT', 'Right', 'Right')
INSERT INTO side (side_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'BILATERAL', 'Bilateral', 'Bilateral')
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (4, GETDATE(), 1, 3,'Upgrade-3')
GO