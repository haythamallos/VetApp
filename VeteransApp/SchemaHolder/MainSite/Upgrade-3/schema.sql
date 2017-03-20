alter table content_type add product_ref_name NVARCHAR(255) NULL
go
alter table content_type add product_ref_description NVARCHAR(255) NULL
go
alter table content_type add number_of_pages INT DEFAULT 0
go

delete from content_type
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price, product_ref_name, product_ref_description, number_of_pages) VALUES (1, GETDATE(), 'BACK', 'Back', 'Back', 40, 0, 99.99, 'OMB Approved No. 2900-0808', 'BACK (THORACOLUMBAR SPINE) CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 11)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price, product_ref_name, product_ref_description, number_of_pages) VALUES (2, GETDATE(), 'SHOULDER', 'Shoulder', 'Shoulder', 30, 1, 99.99, 'OMB Approved No. 2900-0802', 'SHOULDER AND ARM CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 9)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price, product_ref_name, product_ref_description, number_of_pages) VALUES (3, GETDATE(), 'NECK', 'Neck', 'Neck', 30, 0, 99.99, 'OMB Approved No. 2900-0807', 'NECK (CERVICAL SPINE) CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 11)


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

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (4, GETDATE(), 1, 3,'Upgrade-3')
GO