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
	 has_sides                   BIT NULL
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
**		11/26/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'content_state')
  BEGIN
      PRINT 'Dropping Table content_state'

      DROP TABLE content_state
  END

go

CREATE TABLE content_state
  (
     content_state_id NUMERIC(10) NOT NULL PRIMARY KEY,
     date_created    DATETIME NULL,
     code            NVARCHAR(255) NULL,
     [description]     NVARCHAR(255) NULL,
     visible_code    NVARCHAR(255) NULL
  )

go

IF Object_id('content_state') IS NOT NULL
  PRINT '<<< CREATED TABLE content_state >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE content_state >>>'
go


/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		2/16/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'purchase')
  BEGIN
      PRINT 'Dropping Table purchase'

      DROP TABLE purchase
  END

go

CREATE TABLE purchase
  (
     purchase_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
     authtoken      NVARCHAR(255) NULL,
	 is_success                   BIT NULL,
	 is_error                   BIT NULL,
     error_trace         TEXT NULL
  )

go

IF Object_id('purchase') IS NOT NULL
  PRINT '<<< CREATED TABLE purchase >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE purchase >>>'

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
                  AND name = 'content')
  BEGIN
      PRINT 'Dropping Table content'

      DROP TABLE content
  END

go

CREATE TABLE content
  (
     content_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     [user_id] NUMERIC(10) NULL,
     [purchase_id] NUMERIC(10) NULL,
     [content_state_id] NUMERIC(10) NULL,
     [content_type_id] NUMERIC(10) NULL,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
	 content_url        NVARCHAR(255) NULL,
	 content_data           VARBINARY(max) NULL,
	 content_meta        text NULL,
	 is_disabled     BIT NULL,
	 notes        text NULL
  )

go

IF Object_id('content') IS NOT NULL
  PRINT '<<< CREATED TABLE content >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE content >>>'

go


alter table content_type add price DECIMAL(10, 2)
go

alter table [user] add current_rating_back int default 0
go
alter table [user] add current_rating_shoulder int default 0
go
alter table [user] add current_rating_neck int default 0
go
alter table [user] add has_rating_back  BIT NULL
go
alter table [user] add has_rating_shoulder  BIT NULL
go
alter table [user] add has_rating_neck  BIT NULL
go


delete from content_state
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), '0', '0', '0')
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), '25', '25', '25')
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), '50', '50', '50')
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), '75', '75', '75')
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (5, GETDATE(), '100', '100', '100')
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (6, GETDATE(), 'SUBMITTED', 'Content state submitted', 'Content state submitted')
INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (7, GETDATE(), 'PURCHASED', 'Content state purchased', 'Content state purchased')

delete from content_type
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides) VALUES (1, GETDATE(), 'BACK', 'Back', 'Back', 40, 0)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides) VALUES (2, GETDATE(), 'SHOULDER', 'Shoulder', 'Shoulder', 30, 1)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides) VALUES (3, GETDATE(), 'NECK', 'Neck', 'Neck', 30, 0)

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (3, GETDATE(), 1, 2,'Upgrade-2')
GO

