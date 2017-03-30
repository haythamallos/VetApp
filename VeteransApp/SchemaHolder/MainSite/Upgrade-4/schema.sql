
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (4, GETDATE(), 'FOOT', 'Foot', 'Foot', 50, 1, 99.99, 'OMB Approved No. 2900-0810', 'FOOT CONDITIONS, INCLUDING FLATFOOT (PES PLANUS) DISABILITY BENEFITS QUESTIONNAIRE', 10)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (5, GETDATE(), 'SLEEPAPNEA', 'Sleep Apnea', 'Sleep Apnea', 50, 0, 99.99, 'OMB Approved No. 2900-0778', 'SLEEP APNEA DISABILITY BENEFITS QUESTIONNAIRE', 4)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (6, GETDATE(), 'HEADACHE', 'Headache', 'Headache', 50, 0, 99.99, 'OMB Approved No. 2900-0778', 'HEADACHES (INCLUDING MIGRAINE HEADACHES) DISABILITY BENEFITS QUESTIONNAIRE', 4)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (7, GETDATE(), 'ANKLE', 'Ankle', 'Ankle', 20, 1, 99.99, 'OMB Approved No. 2900-0806', 'ANKLE CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 12)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (8, GETDATE(), 'WRIST', 'Wrist', 'Wrist', 20, 1, 99.99, 'OMB Approved No. 2900-0805', 'WRIST CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 10)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (9, GETDATE(), 'KNEE', 'Knee', 'Knee', 20, 1, 99.99, 'OMB Approved No. 2900-0813', 'KNEE AND LOWER LEG CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 11)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (10, GETDATE(), 'HIP', 'Hip', 'Hip', 20, 1, 99.99, 'OMB Approved No. 2900-0811', 'HIP AND THIGH CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 11)
INSERT INTO content_type (content_type_id, date_created, code, [description], visible_code, max_rating, has_sides, price_in_pennies, product_ref_name, product_ref_description, number_of_pages) VALUES (11, GETDATE(), 'ELBOW', 'Elbow', 'Elbow', 20, 1, 99.99, 'OMB Approved No. 2900-0812', 'ELBOW AND FOREARM CONDITIONS DISABILITY BENEFITS QUESTIONNAIRE', 12)
go

alter table jct_user_content_type add RatingLeft INT NULL
go

alter table jct_user_content_type add RatingRight INT NULL
go

INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (5, GETDATE(), 1, 4,'Upgrade-4')
GO