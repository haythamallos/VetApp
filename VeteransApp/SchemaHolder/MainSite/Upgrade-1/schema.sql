
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
     visible_code    NVARCHAR(255) NULL
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
     content_type_id NUMERIC(10) NULL,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
	 content_url        NVARCHAR(255) NULL,
	 content_data           VARBINARY(max) NULL,
	 content_meta        text NULL,
	 is_submitted     BIT NULL,
	 is_disabled     BIT NULL,
	 is_draft     BIT NULL,
     date_submitted  DATETIME NULL,
	 notes        text NULL
  )

go

IF Object_id('content') IS NOT NULL
  PRINT '<<< CREATED TABLE content >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE content >>>'

go

delete from content_type
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'BACK', 'Back content type', 'Back content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'ANKLE', 'Ankle content type', 'Ankle content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'ELBOW', 'Elbow content type', 'Elbow content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'FOOT', 'Foot content type', 'Foot content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (5, GETDATE(), 'HIP', 'Hip content type', 'Hip content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (6, GETDATE(), 'KNEE', 'Knee content type', 'Knee content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (7, GETDATE(), 'SHOULDER', 'Shoulder content type', 'Shoulder content type')
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code) VALUES (8, GETDATE(), 'WRIST', 'Wrist content type', 'Wrist content type')

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (2, GETDATE(), 2, 0,'Upgrade-1')
GO

