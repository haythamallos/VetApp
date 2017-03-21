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

INSERT INTO content_state (content_state_id, date_created, code, [description], visible_code) VALUES (8, GETDATE(), 'DOWNLOADED', 'Content state downloaded', 'Content state downloaded')
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (4, GETDATE(), 1, 3,'Upgrade-3')
GO