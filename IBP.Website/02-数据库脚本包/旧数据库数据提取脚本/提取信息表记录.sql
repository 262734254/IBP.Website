truncate table custom_data_info;

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(NEWID(),'客户等级',Null,'string','customer_level','string',2,20,1,0,GETDATE(),'admin',GETDATE(),null,0)
GO

select * from custom_data_info;
