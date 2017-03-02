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

alter table content_type add price DECIMAL(10, 2)
go

alter table content add  is_purchased BIT NULL
go

alter table content add  date_purchased DATETIME NULL
go

alter table content add  purchase_id [numeric](10, 0) NULL
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (3, GETDATE(), 1, 2,'Upgrade-2')
GO

