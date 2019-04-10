truncate table custom_data_info;
truncate table custom_data_value;
truncate table [pay_pos_machine_info];

--truncate table [product_category_sales_status];
--truncate table [product_category_attributes];
--truncate table [field_type_info];
--truncate table [product_category_attributes];

DECLARE @custom_data_id varchar(50);
select @custom_data_id = newid();
-- select NEWID();


INSERT INTO [pay_pos_machine_info]
           ([pos_machine_id],[pay_from_city_id],[pay_machine_name],[pos_machine_code],[description],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
     VALUES
           ('8B68B6F4-62EE-4867-A249-EAA49887872D','472','深圳工行Pos机1','SHENZHEN-POS-1','深圳工行Pos机1',1,0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
           ('3F1A5AC6-BC8A-4340-83AC-67DBD623280F','472','深圳工行Pos机2','SHENZHEN-POS-1','深圳工行Pos机2',1,0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
           ('23115763-0BC3-4CF6-8CE2-D1BE1F5E18F7','471','广州Pos机1','SHENZHEN-POS-1','广州Pos机',1,0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
           ('FA64D089-C2FD-4A26-9C16-2B43A8E31496','471','广州Pos机2','SHENZHEN-POS-1','广州Pos机',1,0,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);


-- 客户等级 ----------------------------------------------------------
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('E8DCC43F-2823-450B-8ED4-E388F8FD5CA5','客户等级',Null,'custom_data','customer_level','string',2,20,1,0,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'E8DCC43F-2823-450B-8ED4-E388F8FD5CA5',name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 0;


-- 客户来源 ----------------------------------------------------------

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('8D98350B-93AF-432F-88CB-A7381216E61F','客户来源',Null,'custom_data','customer_level','string',2,20,1,1,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'8D98350B-93AF-432F-88CB-A7381216E61F',name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 1;


INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('1A359316-4346-44E3-9D27-B4D0BBC2A45C','8D98350B-93AF-432F-88CB-A7381216E61F','40077项目','4007795588',74, 0, GETDATE(),'admin',null,null,0);
	
-- 工单类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'工单类型',Null,'custom_data','customer_level','string',2,20,1,2,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 2;


-- 工单级别 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'工单级别',Null,'custom_data','customer_level','string',2,20,1,3,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 3;
	
	

-- 证件类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','证件类型',Null,'custom_data','customer_level','string',2,20,1,4,0,GETDATE(),'admin',GETDATE(),null,0)

delete from custom_data_value where data_id = 'E7E9319A-A02B-49DA-9E08-DF9E32AA4C58';

INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('B330B969-C006-477D-A038-9566D71BFD86','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','身份证','C00001',1, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('5500F0D0-B164-4617-9013-0EFB9A74F999','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','警官证','C00002',2, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('FAA2E8B1-7D6B-4B8A-8137-1767D11B4F5E','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','护照','C00003',3, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('B9098EB2-9E45-4731-BE0D-DD081D167CA7','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','军官证','C00004',4, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('8B7CE5EF-BDA3-4990-9577-21B9CF29238F','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','士兵证','C00005',5, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('96E7EF81-5ADD-4F21-AE6E-32FF9824B150','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','其他','C00006',6, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('d8c0f77c-774c-438d-8769-7d6a55a733b3','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','港澳台通行证','C00007',7, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('e73a8c97-277d-4bc8-826b-eddc5fa532d3','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','临时身份证','C00008',8, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('822d4766-de59-447b-bd8b-2dd2b08a107c','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','户口本','C00009',9, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [custom_data_value]([value_id],[data_id],[data_value],[data_value_code],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('5bf1ea51-ccb5-4569-a7d1-eaa9dcb732d2','E7E9319A-A02B-49DA-9E08-DF9E32AA4C58','外国人居留证','C00010',10, 0, getdate(), 'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
	
-- 运营商 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('8AEFCEE9-3699-4F9F-A945-BD4376484402','运营商',Null,'custom_data','customer_level','string',2,20,1,5,0,GETDATE(),'admin',GETDATE(),null,0);

--INSERT INTO [custom_data_value]
--	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
--select 
--	NEWID(),'8AEFCEE9-3699-4F9F-A945-BD4376484402',name, code,showsort,isshow,GETDATE(),'admin',null,null,0
--from
--	txms_db_201206282300.dbo.sys_CodeType where CodeType = 5;
	
INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	('87D5E079-0C68-4F91-B6D6-F639B8B2119A','8AEFCEE9-3699-4F9F-A945-BD4376484402','中国电信', 'C00001',1,0,GETDATE(),'admin',null,null,0),
	('42282285-9409-42F4-94E8-C556DC62B2A5','8AEFCEE9-3699-4F9F-A945-BD4376484402','中国联通', 'C00002',2,0,GETDATE(),'admin',null,null,0),
	('0CC77016-8F5D-4297-B193-1A02B54A750D','8AEFCEE9-3699-4F9F-A945-BD4376484402','中国移动', 'C00003',3,0,GETDATE(),'admin',null,null,0),
	('00C9C4A6-81A3-4CEE-813C-B0965380C089','8AEFCEE9-3699-4F9F-A945-BD4376484402','未知',	   'C00004',4,0,GETDATE(),'admin',null,null,0);

-- 号码类型 --------------------------------------------------------
-- select NEWID();
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('7A7F897F-E023-44C0-BF24-A87436BF70DA','号码类型',Null,'custom_data','customer_level','string',2,20,1,5,0,GETDATE(),'admin',GETDATE(),null,0);

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	('C09DB28A-F4CE-4CD8-A2E9-FFDAACCA7A82','7A7F897F-E023-44C0-BF24-A87436BF70DA','手机号码', 'C00001',1,0,GETDATE(),'admin',null,null,0),
	('5B350231-8FC5-4331-AFED-940C580F79EF','7A7F897F-E023-44C0-BF24-A87436BF70DA','家庭电话', 'C00002',2,0,GETDATE(),'admin',null,null,0),
	('21FAA07F-4F0A-47FD-A5AB-6C0F0A45E831','7A7F897F-E023-44C0-BF24-A87436BF70DA','其他电话', 'C00003',3,0,GETDATE(),'admin',null,null,0),
	('BBFD5F92-E852-4DED-A04B-ACA6C9FAD5F4','7A7F897F-E023-44C0-BF24-A87436BF70DA','业务号码', 'C00004',4,0,GETDATE(),'admin',null,null,0);


-- 号码状态 --------------------------------------------------------
-- select NEWID();
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('EEC83ADF-3C09-43F7-989F-BB1585CEC95A','号码状态',Null,'custom_data','customer_level','string',2,20,1,5,0,GETDATE(),'admin',GETDATE(),null,0);

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	('34B9BC9B-4037-4EA9-8549-C98C11BEE0F9','EEC83ADF-3C09-43F7-989F-BB1585CEC95A','正常', 'C00001',1,0,GETDATE(),'admin',null,null,0),
	('A8D06561-7A3D-44C9-942C-8C40D6D1317F','EEC83ADF-3C09-43F7-989F-BB1585CEC95A','停机', 'C00002',2,0,GETDATE(),'admin',null,null,0),
	('81BD380C-5AFA-4370-98E6-8AEC6164E184','EEC83ADF-3C09-43F7-989F-BB1585CEC95A','拆机', 'C00003',3,0,GETDATE(),'admin',null,null,0);


	
-- 开卡行 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'开卡行',Null,'custom_data','customer_level','string',2,20,1,6,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	case code 
		when 'C00001' then '9344972F-6CFA-45C6-BE8D-DCE22E96D42B' -- 工行
		when 'C00002' then '46483077-55BD-4493-95FD-F7FF172884C8' -- 农行
		when 'C00003' then '9B899374-0D8E-4930-8C9D-4C217937A8A8' -- 建行
		when 'C00004' then '568A8424-7486-421E-BE23-1922EF632DD9' -- 交行
	end,
	@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 6;
	
-- 开卡城市 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'开卡城市',Null,'custom_data','customer_level','string',2,20,1,7,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 7;

-- 托收银行 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'托收银行',Null,'custom_data','customer_level','string',2,20,1,8,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 8;	
		

-- 审批异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'审批异常类型',Null,'custom_data','customer_level','string',2,20,1,9,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 10;	

-- 开卡异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'开卡异常类型',Null,'custom_data','customer_level','string',2,20,1,10,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 11;	

-- 备货异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'备货异常类型',Null,'custom_data','customer_level','string',2,20,1,11,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 12;				
			
-- 发货异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'发货异常类型',Null,'custom_data','customer_level','string',2,20,1,12,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 13;		
				
-- 签收异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'签收异常类型',Null,'custom_data','customer_level','string',2,20,1,13,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 14;		
				
-- 回收异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'回收异常类型',Null,'custom_data','customer_level','string',2,20,1,14,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 15;					

-- 配送公司 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'配送公司',Null,'custom_data','customer_level','string',2,20,1,15,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 16;		
					
-- 广播类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'广播类型',Null,'custom_data','customer_level','string',2,20,1,16,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 17;	
	
	
-- 订单撤消原因 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'订单撤消原因',Null,'custom_data','customer_level','string',2,20,1,17,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 18;	
	
-- 销售撤消原因 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'销售撤消原因',Null,'custom_data','customer_level','string',2,20,1,18,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 19;	
	
-- 号码级别 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'号码级别',Null,'custom_data','customer_level','string',2,20,1,19,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 20;	
		
-- 订单失败原因 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'订单失败原因',Null,'custom_data','customer_level','string',2,20,1,20,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 21;		


-- 订单来源 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'订单来源',Null,'custom_data','customer_level','string',2,20,1,21,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 22;	

-- 通讯消费 ----------------------------------------------------------
-- select NEWID();
-- select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('1B714EA9-EE6B-4D10-AEA1-AB04DCCD6287','通讯消费',Null,'custom_data','customer_level','string',2,20,1,22,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'1B714EA9-EE6B-4D10-AEA1-AB04DCCD6287', name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 24;	
	
			
INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	('A22C0D97-37B5-4430-9842-3A3C80DAC391','1B714EA9-EE6B-4D10-AEA1-AB04DCCD6287', '未知', 'C00008',8,0,GETDATE(),'admin',null,null,0)			

-- 优选品牌 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('D61C6028-CDEC-43E9-B2D9-721A9E0570A0','优选品牌',Null,'custom_data','customer_level','string',2,20,1,23,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'D61C6028-CDEC-43E9-B2D9-721A9E0570A0',name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 25;	
	

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES( 
	'8B5596D1-2848-426F-9616-D42FC5AFA17A','D61C6028-CDEC-43E9-B2D9-721A9E0570A0','未知', 'C00014',14,0,GETDATE(),'admin',null,null,0)	
	
	
-- 手机价位 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('EDF77C7A-49FE-4749-9BCA-3CA2016AB251','手机价位',Null,'custom_data','customer_level','string',2,20,1,24,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),'EDF77C7A-49FE-4749-9BCA-3CA2016AB251',name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 26;	
	

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES( 
	'3DB0973E-66BE-4196-928B-7C5F363B762F','EDF77C7A-49FE-4749-9BCA-3CA2016AB251','未知', 'C00015',15,0,GETDATE(),'admin',null,null,0);

		
-- 销售城市 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'销售城市',Null,'custom_data','customer_level','string',2,20,1,25,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	txms_db_201206282300.dbo.sys_CodeType where CodeType = 27;	
	

-- 工作状态 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'工作状态',Null,'custom_data','customer_level','string',2,20,1,26,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('8FA195F5-21B4-45D8-8472-23E013B336BA',@custom_data_id,'试用','WorkStatus001',0,0,GETDATE(),'admin',null,null,0),
	('DD1C5D4E-46F9-4535-8462-661534DC2A1A',@custom_data_id,'转正','WorkStatus002',1,0,GETDATE(),'admin',null,null,0),
	('2CA85927-4E1E-408A-8E44-A3A24F1046D5',@custom_data_id,'离职','WorkStatus003',2,0,GETDATE(),'admin',null,null,0);
											
-- select c.data_name, v.* from custom_data_value v inner join custom_data_info c on v.data_id = C.data_id where v.data_id = @custom_data_id order by v.sort_order 


-- 银行卡功能鉴别

-- select newid();

INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('19603784-CCB1-4611-BD44-85DD8554803E','银行卡功能鉴别',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('FEB39D81-26EC-4A20-97F2-F148FDC87AFD','19603784-CCB1-4611-BD44-85DD8554803E','分期','BankFunction001',0,0,GETDATE(),'admin',null,null,0),
	('3A79E7D2-81E3-4D8B-BBB3-E1FD07E5E717','19603784-CCB1-4611-BD44-85DD8554803E','不可分期','BankFunction002',1,0,GETDATE(),'admin',null,null,0),
	('07A65711-5D33-4CC4-994D-BC266BB5ACC7','19603784-CCB1-4611-BD44-85DD8554803E','不能透支','BankFunction003',2,0,GETDATE(),'admin',null,null,0),
	('D3732C88-7C6C-4A5E-872D-710CAB71C003','19603784-CCB1-4611-BD44-85DD8554803E','其他','BankFunction004',3,0,GETDATE(),'admin',null,null,0);
		
GO

 -- 扣款异常类型
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('B00B664D-450F-41E7-B3C6-84B67BC00C04','扣款异常类型',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('BE25C8E9-17D4-462F-AE2C-F667467A0628','B00B664D-450F-41E7-B3C6-84B67BC00C04','扣款异常类型1','BankFunction001',0,0,GETDATE(),'admin',null,null,0),
	('E10D6622-0488-4976-942A-2D676C1657CB','B00B664D-450F-41E7-B3C6-84B67BC00C04','扣款异常类型2','BankFunction002',1,0,GETDATE(),'admin',null,null,0),
	('62B972A1-22C4-4D8C-977F-EA491F97CDC9','B00B664D-450F-41E7-B3C6-84B67BC00C04','扣款异常类型3','BankFunction003',2,0,GETDATE(),'admin',null,null,0),
	('DB86A334-4D21-4755-9D5D-304E5C710B35','B00B664D-450F-41E7-B3C6-84B67BC00C04','扣款异常类型4','BankFunction004',3,0,GETDATE(),'admin',null,null,0);


 -- 质检异常类型
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('EC6CCC9A-8BB6-4E51-BEC6-D6A8D2555162','质检异常类型',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('47097AD4-2013-4E72-A02C-69DCA71B52B5','EC6CCC9A-8BB6-4E51-BEC6-D6A8D2555162','质检异常类型1','BankFunction001',0,0,GETDATE(),'admin',null,null,0),
	('D07A615E-D508-42F1-9CD3-B5F07FEECFE3','EC6CCC9A-8BB6-4E51-BEC6-D6A8D2555162','质检异常类型2','BankFunction002',1,0,GETDATE(),'admin',null,null,0),
	('AB6F65D9-F3C9-412F-A850-C0B8D80CD0AC','EC6CCC9A-8BB6-4E51-BEC6-D6A8D2555162','质检异常类型3','BankFunction003',2,0,GETDATE(),'admin',null,null,0),
	('7A001B92-891D-4AEE-B217-CC3194681BD9','EC6CCC9A-8BB6-4E51-BEC6-D6A8D2555162','质检异常类型4','BankFunction004',3,0,GETDATE(),'admin',null,null,0);

 -- 退款异常类型
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('9E8B4176-95AD-48A6-99D8-A2E3898EFED5','退款异常类型',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('6C05532D-A544-4B84-99A2-92C890219C92','9E8B4176-95AD-48A6-99D8-A2E3898EFED5','退款异常类型1','BankFunction001',0,0,GETDATE(),'admin',null,null,0),
	('A2966617-0524-4E3C-8914-E20BEC15866B','9E8B4176-95AD-48A6-99D8-A2E3898EFED5','退款异常类型2','BankFunction002',1,0,GETDATE(),'admin',null,null,0),
	('0CBC51FD-19F6-45CE-AA37-585F00143EF4','9E8B4176-95AD-48A6-99D8-A2E3898EFED5','退款异常类型3','BankFunction003',2,0,GETDATE(),'admin',null,null,0),
	('741B5B8C-BE50-4029-8AC0-A28F6692B048','9E8B4176-95AD-48A6-99D8-A2E3898EFED5','退款异常类型4','BankFunction004',3,0,GETDATE(),'admin',null,null,0);

 -- 销户异常类型
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('5FD796B9-31FA-4800-BB42-C5B2948310DB','销户异常类型',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('FAEAC63F-F2B0-44DB-8EE0-79A242584CB1','5FD796B9-31FA-4800-BB42-C5B2948310DB','销户异常类型1','BankFunction001',0,0,GETDATE(),'admin',null,null,0),
	('6C163D08-93E5-466B-9CAE-F1B80E2473D6','5FD796B9-31FA-4800-BB42-C5B2948310DB','销户异常类型2','BankFunction002',1,0,GETDATE(),'admin',null,null,0),
	('6BD9281B-B4B4-4A3B-B7D0-F363D9C40D91','5FD796B9-31FA-4800-BB42-C5B2948310DB','销户异常类型3','BankFunction003',2,0,GETDATE(),'admin',null,null,0),
	('31A2FAA3-9C3A-4D6C-B09A-2ACBACA8DCA6','5FD796B9-31FA-4800-BB42-C5B2948310DB','销户异常类型4','BankFunction004',3,0,GETDATE(),'admin',null,null,0);

 -- 退货异常类型
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('A41F83DF-CC3C-4E81-AABA-2CA7ADAC4C9D','退货异常类型',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('BF17875F-6024-4A75-B642-67CA74457721','A41F83DF-CC3C-4E81-AABA-2CA7ADAC4C9D','退货异常类型1','BankFunction001',0,0,GETDATE(),'admin',null,null,0),
	('8A3EAFD8-F5DE-4559-88ED-C67E75697B36','A41F83DF-CC3C-4E81-AABA-2CA7ADAC4C9D','退货异常类型2','BankFunction002',1,0,GETDATE(),'admin',null,null,0),
	('F4208EB3-8735-4CE2-849B-F8D3D23A4FF0','A41F83DF-CC3C-4E81-AABA-2CA7ADAC4C9D','退货异常类型3','BankFunction003',2,0,GETDATE(),'admin',null,null,0),
	('85AF666E-D200-4901-8537-1B4BE5C07772','A41F83DF-CC3C-4E81-AABA-2CA7ADAC4C9D','退货异常类型4','BankFunction004',3,0,GETDATE(),'admin',null,null,0);


-- select NEWID();	
-- 联系记录-》联系结果
										
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','联系记录(联系结果)',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0);

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','本人接听','ContactResult001',1,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','无人接听','ContactResult002',2,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','关机','ContactResult003',3,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','停机','ContactResult004',4,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','呼叫转移','ContactResult005',5,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','忙音占线','ContactResult006',6,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','不在服务区','ContactResult007',7,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','直接挂机','ContactResult008',8,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','余额不足','ContactResult009',9,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','空号过期无效','ContactResult010',10,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','语言不通','ContactResult011',11,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','非本人','ContactResult012',12,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'FD440BAD-D22E-402A-8AEB-EF4EB526ECFB','其他','ContactResult013',13,0,GETDATE(),'admin',null,null,0);
		
GO

-- select NEWID();	
-- 联系记录-》联系目的
										
INSERT INTO custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	('86DA9C67-C2C2-44C3-8D27-488523ADB4C6','联系记录(联系目的)',Null,'custom_data','customer_level','string',2,20,1,27,0,GETDATE(),'admin',GETDATE(),null,0);

INSERT INTO [custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	(NEWID(),'86DA9C67-C2C2-44C3-8D27-488523ADB4C6','咨询订购','ContactPurpose001',1,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'86DA9C67-C2C2-44C3-8D27-488523ADB4C6','工单处理','ContactPurpose002',2,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'86DA9C67-C2C2-44C3-8D27-488523ADB4C6','我的营销','ContactPurpose003',3,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'86DA9C67-C2C2-44C3-8D27-488523ADB4C6','外呼营销','ContactPurpose004',4,0,GETDATE(),'admin',null,null,0),
	(NEWID(),'86DA9C67-C2C2-44C3-8D27-488523ADB4C6','WelcomeCall','ContactPurpose005',5,0,GETDATE(),'admin',null,null,0);
	
GO


truncate table [bankcard_type_info];


INSERT INTO [bankcard_type_info]
           ([bankcard_type_id]
           ,[bank_enum_value]
           ,[card_bin_code]
           ,[card_type]
           ,[card_nature]
           ,[card_brand]
           ,[currency_type]
           ,[card_level]
           ,[bankcard_enum_value]
           ,[created_on]
           ,[created_by]
           ,[modified_on]
           ,[modified_by]
           ,[status_code])
     SELECT 
           NEWID(),
           case yh
				when 'C00001' then '9344972F-6CFA-45C6-BE8D-DCE22E96D42B' -- 工行
				when 'C00002' then '46483077-55BD-4493-95FD-F7FF172884C8' -- 农行
				when 'C00003' then '9B899374-0D8E-4930-8C9D-4C217937A8A8' -- 建行
				when 'C00004' then '568A8424-7486-421E-BE23-1922EF632DD9' -- 交行
		   end
           ,[BinNum]
           ,[KType]
           ,[KState]
           ,[KPP]
           ,[BZ]
           ,[KJB],
           case  [KGN] 
			when 0 then 'FEB39D81-26EC-4A20-97F2-F148FDC87AFD' 
			when 1 then '3A79E7D2-81E3-4D8B-BBB3-E1FD07E5E717'
			when 2 then '07A65711-5D33-4CC4-994D-BC266BB5ACC7'
			when 3 then 'D3732C88-7C6C-4A5E-872D-710CAB71C003'
           end
           ,GETDATE()
           ,'admin'
           ,null
           ,null
           ,0
      from txms_db_201206282300.dbo.BD_CardInfo 
      where txms_db_201206282300.dbo.BD_CardInfo.YH is not null
GO


truncate table [department_info];

INSERT INTO [department_info]
	([sort_order],[department_id],[parent_id],[department_name],[description],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(0,'1D8352A7-1EA6-45A1-86FA-23171B962A3E',null,'总经办',null,GETDATE(),'admin',null,null,0),
	(1,'2F13F5DD-EBD3-4CE2-8E12-4261F883AF50',null,'战略发展中心',null,GETDATE(),'admin',null,null,0),
	(2,'A7611285-8243-48E7-AD90-F083F57B152E',null,'营销中心',null,GETDATE(),'admin',null,null,0),

	(201,'7A96841D-5503-4DF3-B1DB-22F8448BFF50','A7611285-8243-48E7-AD90-F083F57B152E','质控部',null,GETDATE(),'admin',null,null,0),
	(202,'AFBD40DA-3934-4C73-89DA-395B277B708F','A7611285-8243-48E7-AD90-F083F57B152E','物流部',null,GETDATE(),'admin',null,null,0),
	(203,'C30FC6C1-0DD9-4D81-A8CC-BDD1295CD652','A7611285-8243-48E7-AD90-F083F57B152E','客户联络中心',null,GETDATE(),'admin',null,null,0),


	(3,'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',null,'技术部',null,GETDATE(),'admin',null,null,0),
	(4,'12F679F9-441B-4708-8FB1-17E29AA601C3',null,'人力资源部',null,GETDATE(),'admin',null,null,0),
	(5,'DB3404A3-0019-49F8-ADC0-23DCD55C4251',null,'财务部',null,GETDATE(),'admin',null,null,0)
	
	
GO

truncate table [role_info];

insert into [role_info] 
	(role_id, role_name, cn_name, description, role_status, created_on, created_by, modified_on, modified_by, status_code) 
values 
	('656AECDA-717F-4CB8-A9A9-E7D0C75E82CF','管理员','管理员', null, 0, getdate(),'admin',null,null,0),
	('fd0aea4f-1ca0-42da-a25f-243dc10aaee5','4077项目客服','4077项目客服', null, 0, getdate(),'admin',null,null,0);
GO



truncate table [action_info];
INSERT INTO [action_info]
	([action_id],[node_id],[sort_order],[parent_node],[action_name],[action_type],[action_group],[display_name],[controller_name],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	-- 呼叫中心
	('CEF9C0CD-DBA5-46C7-95A4-ACBCFABB222B',1,1,0,'CallCenter_Index',0,'CallCenter_Index','呼叫中心','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 呼叫中心 子级菜单
	('F26FF936-795C-40DB-BEEB-F3938936AB43',101,101,1,'CustomerMgr_Index',0,'CustomerMgr_Index','客户管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 呼叫中心->客户管理 子级菜单
	('6B606F4B-80FD-4FE8-970F-FB9711101B90',10101,10101,101,'CustomerMgr',0,'CustomerMgr','客户信息管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 呼叫中心->客户管理->客户信息管理 子级菜单
	('26348B57-FFD3-4E7E-A785-76751057B94B',1010101,1010101,10101,'AddCustomerInfo',0,'CustomerMgr','新增客户信息','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('FE97D766-6820-4E0A-8F41-341575EDAD59',1010102,1010102,10101,'NewCustomerMemo',0,'CustomerMgr','新增客户备注视图','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('ECCBC5EE-AEB5-4E8C-B133-FF1E7A354138',1010103,1010103,10101,'NewCustomerContact',0,'CustomerMgr','新增联系记录视图','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('4171F99A-6EEE-47D4-9F41-DACFDAC5F010',1010104,1010104,10101,'DoAddCustomerInfo',0,'CustomerMgr','新增客户信息操作','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('BBDF1FBC-DEB7-42E1-BF6F-F9765FA3FD3E',1010105,1010105,10101,'CustomerInfo',0,'CustomerMgr','客户详细信息视图','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),



	('CB157FFC-81F7-437D-B5E4-9884D2E2D20E',10102,10102,101,'CustomerApprovalMgr',0,'CustomerApprovalMgr','客户信息审核管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('D0CB5F25-997C-4603-BFF8-71FDCFE647BE',1010201,1010101,10102,'CustomerInfoApproval',1,'CustomerApprovalMgr','客户信息审核视图','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 呼叫中心->客户管理->客户属性信息管理
	('343EECE6-5AD6-44B9-9997-FDD5ED8BECF5',10103,10103,101,'CustomerAttributeGroupMgr',0,'CustomerAttributeGroupMgr','客户属性信息分组','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('9763FCA9-D87D-4BB2-9DC9-5DB4B2F51EFB',10104,10104,101,'CustomerAttributeMgr',0,'CustomerAttributeGroupMgr','客户属性信息管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 呼叫中心->外呼管理 子级菜单
	('015E32DB-ACE6-403E-95BF-5EEE71153F54',102,102,1,'OutDialer_index',0,'OutDialer','外呼管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('D92AC6AC-81C8-45C9-BA86-DA2D81D3291A',10201,10201,102,'AutoDialerTaskMgr',0,'OutDialer','自动外呼任务管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A669DF0E-9715-4746-992D-22044142754E',10202,10202,102,'AutoOutDialerResult',0,'OutDialer','自动外呼结果查询','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('32C4E913-4604-40B0-87AD-98CBE2F0D163',10203,10203,102,'AutoOutDialerConfig',0,'OutDialer','自动外呼参数配置','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('E4A297EF-A143-418B-996E-61795CFD7952',103,103,1,'Group77_index',0,'Group77','77项目组管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('16CCA61D-A509-48F4-A1E0-734BEDBB69C5',10301,10301,103,'IncomeCallForGroup77',1,'Group77','77项目来电处理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('2221A9ED-2F99-49E7-A91E-3938F7C74D3C',10302,10302,103,'CustomerMgrForGroup77',0,'Group77','77项目客户管理','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

-- select newid();
	-- 订单中心	
 	('D5C19543-5B8B-451D-9CF9-96729F84E77D',2,2,0,'OrderCenter_Index',0,'OrderCenter_Index','订单中心','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
 
	('826AEC41-DD50-4A4B-B9C3-8FB5A0BAD6BD',201,201,2,		'MySalesOrder',			0,'MySalesOrder','我的销售订单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('619F64CE-8836-4B0B-8289-8919F1937E1B',20101,20101,201,'MyFollowOrder',		0,'MySalesOrder','待跟进订单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('1733E648-F99B-44BB-A4CF-94E88F052E2D',20102,20102,201,'MyExceptionOrder',		0,'MySalesOrder','异常订单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

-- select newid();
	('AA8E4B06-7097-4F6A-A1E3-632B0532AB3E',202,202,2,		'SaleOrderProcess',		0,'SaleOrderProcess','订单处理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),	
	
	('0D06F3BB-0C69-4EC9-A459-070495FB1013',20201,20201,202,'WaitCheckOrder',				0,'WaitCheckOrder','待质检单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('1743CAC7-9CD0-48A4-A887-4ED9E1342A46',2020101,2020101,20201,'SalesOrderCheck',		1,'WaitCheckOrder','订单质检视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('39B8C60C-32A4-4417-B583-05980862275E',2020102,2020102,20201,'DoSalesOrderCheck',		1,'WaitCheckOrder','订单质检操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('CB7F4146-DEC6-43D9-9E2A-758CBC5E5115',20202,20202,202,'WaitChargeOrder',				0,	'WaitChargeOrder','待扣款单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('FD5D9222-0DCC-4373-9181-9D133862F5E3',2020201,2020201,20202,'SalesOrderCharge',		1,	'WaitChargeOrder','订单扣款视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('54CED203-2F07-4335-8B0C-61E60CF52D0A',2020202,2020202,20202,'DoSalesOrderCharge',		1,	'WaitChargeOrder','订单扣款操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('13E63DB8-ABAB-40C4-B273-5355F708E574',20203,20203,202,'WaitApprovalOrder',			0,'WaitApprovalOrder','待审批单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('EC2B37E1-9E7E-45DC-BF8E-18D2EE4C7161',2020301,2020301,20203,'SalesOrderApproval',		1,'WaitApprovalOrder','订单审批视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('876A726F-3CC0-4261-8C9A-85C4E751E52B',2020302,2020302,20203,'DoSalesOrderApproval',	1,'WaitApprovalOrder','订单审批操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('1DBEB1C7-84ED-48D4-9AAF-D524E0DAF27C',20204,20204,202,'WaitOpeningOrder',				0,'WaitOpeningOrder','待开户单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('53F89065-16F7-4CC5-B5B2-7C71812297C0',2020401,2020401,20204,'SalesOrderOpening',		1,'WaitOpeningOrder','订单开户视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('B35D4E8B-5525-47D4-BFAC-748BAC3F3940',2020402,2020402,20204,'DoSalesOrderOpening',	1,'WaitOpeningOrder','订单开户操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('AE47111A-501D-4546-A00F-40F481F45CA9',20205,20205,202,'WaitStockingOrder',			0,'WaitStockingOrder','待备货单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('AE33FFED-CBAA-49E3-8588-D6D3F969954F',2020501,2020501,20205,'SalesOrderStocking',		1,'WaitStockingOrder','订单备货视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('CB35E022-B457-4308-868C-8F4A274E9775',2020502,2020502,20205,'DoSalesOrderStocking',	1,'WaitStockingOrder','订单备货操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('CBA1DAA0-C90D-4103-88E2-7D6D6E1BE294',20206,20206,202,'WaitDeliveryOrder',			0,'WaitDeliveryOrder','待发货单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A809A622-926C-4FB1-A76B-E52B10E8AC82',2020601,2020601,20206,'SalesOrderDelivery',		1,'WaitDeliveryOrder','订单发货视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('8C8A81E0-2657-42D0-9A22-A748503EEFE3',2020602,2020602,20206,'DoSalesOrderDelivery',	1,'WaitDeliveryOrder','订单发货操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('A16B4F92-C67B-4395-9858-9F4F17BF9572',20207,20207,202,'WaitSignOrder',				0,'WaitSignOrder','待签收单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('82F40F44-2195-456E-A0FE-54974E1483B6',2020701,2020701,20207,'SalesOrderSign',			1,'WaitSignOrder','订单签收视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('B3476C45-FB2F-407B-8439-AD833C08AB52',2020702,2020702,20207,'DoSalesOrderSign',		1,'WaitSignOrder','订单签收操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('6DF2371A-157E-4475-B61E-98CC35D9C0A0',20208,20208,202,'WaitRecoverOrder',				0,'WaitRecoverOrder','待回收单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('7CDD763F-C445-41E0-BDD0-C3A409AA5636',2020801,2020801,20208,'SalesOrderRecover',		1,'WaitRecoverOrder','订单回收视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('6A438AD7-0B88-458A-9010-C4056431CABE',2020802,2020802,20208,'DoSalesOrderRecover',	1,'WaitRecoverOrder','订单回收操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('496C5BB4-9658-4615-B85C-4CA1BFE15077',20209,20209,202,'WaitRefundOrder',				0,'WaitRefundOrder','待退款单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('884C6252-2020-45BD-96B9-D56C0E746A28',2020901,2020901,20209,'SalesOrderRefund',		1,'WaitRefundOrder','订单退款视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('22BC464E-0EA8-4014-ACFF-E94D0B266EBB',2020902,2020902,20209,'DoSalesOrderRefund',		1,'WaitRefundOrder','订单退款操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('B0861355-E440-4E22-A7A2-2FBF5B3466A2',20210,20210,202,'WaitReturnsOrder',				0,'WaitReturnsOrder','待退货单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('DC3DB390-AF8E-4787-B206-C521DE522CE3',2021001,2021001,20210,'SalesOrderReturn',		1,'WaitReturnsOrder','订单退货视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('5B0679FA-666B-45C8-B3B6-42A2B672F045',2021002,2021002,20210,'DoSalesOrderReturn',		1,'WaitReturnsOrder','订单退货操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
 
	('1E9C8D81-8D55-42E0-A2F1-F371E4A378D8',20211,20211,202,		'WaitCancelOpeningOrder',		0,'WaitCancelOpeningOrder','待消户单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('6890B385-F148-4D6C-9367-5B18D94659CD',2021101,2021101,20211,	'SalesOrderCancelOpening',		1,'WaitCancelOpeningOrder','订单消户视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('274ADB3D-453D-410A-85EB-9A9E19CB59E5',2021102,2021102,20211,	'DoSalesOrderCancelOpening',	1,'WaitCancelOpeningOrder','订单消户操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	('E82CA98C-17E1-4614-9BDE-B5A35FA9B4D1',203,203,2,				'OrderManager_Index',			0,'OrderManager_Index','订单管理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A942366A-6EBF-4C0A-A728-E648F6533040',20301,20301,203,		'OrderManager',			0,'OrderManager','订单信息管理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	('8BBE19E2-835D-483B-AA4F-9DE009C5B54B',2030101,2030101,20301,	'SalesOrderDetail',		1,'OrderManager','订单详细信息','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('58D3C747-B0BD-4BC4-8A2C-95D241131594',2030102,2030102,20301,	'UpdateSalesorderInfo',	1,'OrderManager','更新订单视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('AB43F2C2-4C7D-4567-9707-B7F052FCB4CC',2030103,2030103,20301,	'DoUpdateSalesOrder',	1,'OrderManager','更新订单操作','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('436678C9-791E-4140-9B4D-4480673633EB',2030204,2030204,20301,	'DoCancelSalesOrder',	1,'OrderManager','撤消订单','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('1105DC37-49A9-4B28-996B-CCC8EDB56161',2030205,2030205,20301,	'SalesOrderCancel',		1,'OrderManager','订单撤消视图','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	('332472CF-5F94-429B-9526-1A54327C28D0',20302,20302,203,		'SalesOrderTypeManager',0,'OrderManager_Index','订单类型管理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('2AF9BB3A-634C-4041-B9BF-01FCD9BDF024',20303,20303,203,		'RevokedOrder',			0,'OrderManager_Index','撤消单管理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),	
	('ADEB0949-DBD2-4E22-9E83-9950E382C92D',20304,20304,203,		'AllExceptionOrder',	0,'OrderManager_Index','异常单管理','OrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	


 

	-- 工单中心	
	('AA1D37A5-3CB2-49D2-8B01-BBDEA06803A1',3,3,0,'WorkOrderCenter_Index',0,'WorkOrderCenter_Index','工单中心','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 工单中心 子级菜单	
	('CD7302AD-23C1-4010-BBCD-0D9636F988CD',301,301,3,'NewWorkOrder',0,'NewWorkOrder','新建工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('E95C3447-8A16-4003-A57F-A9225C43A0E9',302,302,3,'PendingWorkOrderInGroup',0,'PendingWorkOrderInGroup','本组待处理工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	
	
	('E051D98E-0D52-4BEC-969B-B86B394031E9',303,303,3,'PendingWorkOrder',0,'PendingWorkOrder','待处理工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('9F229921-3DF6-4475-B680-0DF8F65A7EFD',304,304,3,'ProcessingWorkOrder',0,'ProcessingWorkOrder','处理中工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('244ECA64-352C-4819-87FC-34CE6856EE02',305,305,3,'WaittingApprovalWorkOrderForMe',0,'WaittingApprovalWorkOrderForMe','待审批工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('2C8FCE48-E4D9-4A23-961C-4BB9F2E46171',306,306,3,'ClosedWorkOrderForMe',0,'ClosedWorkOrderForMe','已关闭工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('592B180E-C8D8-41EE-B7E3-6398FD21A4BC',307,307,3,'QualityCheckedWorkOrderForMe',0,'QualityCheckedWorkOrderForMe','已质检工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('82A838F2-D17F-41C1-91A5-42514D3ED97E',308,308,3,'WorkOrderApprovalMgr',0,'WorkOrderApprovalMgr','工单审批管理','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('BEB8CA3C-632C-430E-A17E-F9ADF7782E64',30801,30801,3,'DoWorkOrderApproval',1,'WorkOrderApprovalMgr','执行工单审批操作','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('DA047F0C-23CF-45C2-8EC4-7C34086CD14E',30802,30802,3,'DoSubmitApprovalWorkOrder',1,'WorkOrderApprovalMgr','提交工单审批操作','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('44B078E2-D560-45B8-B74F-EFA6BB9D99C1',30803,30803,3,'NewWorkOrderApproval',1,'WorkOrderApprovalMgr','工单审批视图','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('7BA2271C-F783-4969-AD2F-BD3B54BA88A4',309,309,3,'WorkOrderQualityCheckedMgr',0,'WorkOrderQualityCheckedMgr','工单质检管理','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('144841EA-383D-46E3-9AE3-6A36C5A089DA',30901,30901,3,'NewWorkOrderQualityChecked',1,'WorkOrderQualityCheckedMgr','工单质检视图','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('63EC2FFE-A7CE-408B-9C6E-0E443D749209',30902,30902,3,'DoNewWorkOrderQualityChecked',1,'WorkOrderQualityCheckedMgr','添加工单质检记录','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('DC845A6D-8758-44DE-B164-52DD96ACD73A',30903,30903,3,'DoSubmitQualityCheckedWorkOrder',1,'WorkOrderQualityCheckedMgr','提交工单质检操作','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('D283D03D-C3AC-42FA-9211-70DA1C4A8C53',310,310,3,'WorkOrderManager',0,'WorkOrderManager','工单管理','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('32ADAD27-2B3A-4BB4-9DFC-47060F6FA5F3',311,311,3,'WorkOrderTypeMgr',0,'WorkOrderTypeMgr','工单类型管理','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A24A7B58-7747-4D99-A093-112F5811D4D6',31101,31101,3,'WorkOrderTypeDetails',1,'WorkOrderTypeMgr','工单类型信息','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('602E1A79-A80B-40A8-8B7A-C318462F97A0',31102,31102,3,'DoDeleteWorkOrderTypeStatus',1,'WorkOrderTypeMgr','删除工单类型状态','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('326E1EE0-A4DE-46D8-9B08-ACD6975BB9E9',31103,31103,3,'DoDeleteWorkOrderTypeResult',1,'WorkOrderTypeMgr','删除工单处理结果','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('54399838-AD27-4CC9-BD27-FEFDCA482496',31104,31104,3,'DoMoveUpWorkOrderTypeStatus',1,'WorkOrderTypeMgr','上移工单类型状态','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('63CE948B-8ADA-4F1B-B0BE-89ECE4729C3C',31105,31105,3,'DoMoveDownWorkOrderTypeStatus',1,'WorkOrderTypeMgr','下移工单类型状态','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('0FA4D1F6-E8EF-4DA4-8DB8-8039DE9781E1',31106,31106,3,'DoMoveUpWorkOrderTypeResult',1,'WorkOrderTypeMgr','上移类型处理结果','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('7F7AAF18-F3B9-4C18-92A1-8A9554034FEA',31107,31107,3,'DoMoveDownWorkOrderTypeResult',1,'WorkOrderTypeMgr','下移类型处理结果','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('57E5D4C2-2A0B-4013-975E-FE9363B37C59',31108,31108,3,'AddWorkOrderTypeResult',1,'WorkOrderTypeMgr','添加工单处理结果','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('6CEE9E3A-1C93-484D-A7A6-943A7B5D5AB0',31109,31109,3,'AddWorkOrderTypeStatus',1,'WorkOrderTypeMgr','添加工单类型状态','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A337ECF6-568F-47E5-A72E-8EFCA9B3CA8A',31110,31110,3,'DoNewWorkOrderTypeStatus',1,'WorkOrderTypeMgr','新建工单类型状态','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('2946B655-1D63-455E-A4EF-9FC1FF3F4F7C',31111,31111,3,'DoNewWorkOrderTypeResult',1,'WorkOrderTypeMgr','新建类型处理结果','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	('06219FDE-EC34-4C06-AA77-1317DEB7E26E',312,312,3,'WorkOrderDetail',1,'WorkOrderManager','工单详细信息','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('131A09F5-D530-4F5F-9E90-E1C4AD75B856',31201,31201,3,'NewWorkOrderProcessRecord',1,'WorkOrderManager','新增工单处理记录','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('87B7F5E5-D597-4715-82EB-DD8BD1724746',31202,31202,3,'WorkOrderAssignment',1,'WorkOrderManager','工单转交视图','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('37ACD722-CF47-407F-A6CB-3CDE1C9307E1',31203,31203,3,'DoAssignmentWorkOrder',1,'WorkOrderManager','执行工单转交操作','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('2AAD6D70-FF4E-4D08-8E65-87462845A3DD',31204,31204,3,'DoChangeWorkorderExpiredTime',1,'WorkOrderManager','更新工单过期时间','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('615C7FAC-4A19-433C-92A1-715B0B0FFDDC',31205,31205,3,'DoChangeWorkorderAdvanceTime',1,'WorkOrderManager','更新工单预约时间','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('081E7571-4465-4F41-B107-7926373C09CC',31206,31206,3,'DoNewWorkOrderProcessRecord',1,'WorkOrderManager','新增工单处理记录','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('89B2A15E-F2CD-4F81-ABE4-05949597673E',31207,31207,3,'DoCloseWorkOrder',1,'WorkOrderManager','关闭工单','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('8F2ED1F7-4894-4435-9C06-7DB8480F92B5',31208,31208,3,'ChangeWorkorderExpiredTime',1,'WorkOrderManager','工单过期时间视图','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('1ADB26D8-BE65-45B9-8D8D-45C05BA953DB',31209,31209,3,'ChangeWorkorderAdvanceTime',1,'WorkOrderManager','工单预约时间视图','WorkOrderCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 产品中心
	('D921AFCD-1583-46F9-9040-E584D42281CE',4,4,0,'ProductCenter',0,'ProductCenter','产品中心','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),

-- 产品中心-> 产品类型配置管理 二级菜单 
	('4FD54F21-ED38-4C92-B894-C23F3069369F',401,401,4,'ProductCategoryMgr_index',0,'ProductCenter','产品配置管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A099AF20-E0BD-461B-A56E-420B376A4FAF',40101,40101,401,'ProductCategoryGroupMgr',0,'ProductCenter','品类分组管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('9ED01FE7-FB62-483B-8D43-41C1564886DF',40102,40102,401,'ProductCategoryMgr',0,'ProductCenter','产品品类管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('BABD26C3-CE55-4E01-8A7A-11309E635880',40103,40103,401,'ProductCategoryAttribute',0,'ProductCenter','品类属性管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('E93DEEDF-9EB3-47C1-BD96-D247C2675A01',40104,40104,401,'ProductCategorySaleStatus',0,'ProductCenter','销售状态管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('64F58DE2-312A-451E-AD3B-934C457DD0B6',402,402,4,'ProductStockMgr_index',0,'ProductCenter','产品库存管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('B236E3DA-AC11-4234-8FCD-CB5C4BF481C7',40201,40201,402,'ProductStockMgr',0,'ProductCenter','产品入库管理','ProductCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 业务中心	
	('AB0F7933-3D9B-40D9-86B5-8EA98EBA43E4',5,5,0,'BusinessCenter_Index',0,'BusinessCenter_Index','业务中心','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	--('4625BC5E-DAAD-4CDF-A85B-926702CAB3E1',502,502,5,'NumberQuery',0,'NumberQuery','号码查询','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('B1887FBE-AC54-4002-A2F1-53B75B869D6F',503,503,5,'OrderQuery',0,'OrderQuery','订单查询','CallCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 业务中心 子级菜单	
	('B9951620-D66A-4DDA-AF52-4D82C2E7EA3E',504,504,5,'SalePackageManager',0,'SalePackageManager','营销产品包管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('A5EFB5B1-9924-4988-A3F5-5B6819CC1CF5',505,505,5,'CustomInfo',0,'CustomInfo','业务信息管理','System',GETDATE(),'admin',GETDATE(),'admin',0),
	('5A572AB9-CCEC-470F-BA12-3D3B6165A1C5',506,506,5,'BankCardInfo',0,'BankCardInfo','银行卡信息管理','System',GETDATE(),'admin',GETDATE(),'admin',0),
	('A165E5F0-F2FF-4D29-AAA5-9988CE91D7C2',507,507,5,'Questionnaire',0,'Questionnaire','调查问卷管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('000C77B6-19E2-4C9E-BB57-FB3A139ADB1D',50701,50701,507,'QuestionLib',0,'QuestionLib','题库管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('2D1DA17D-CD2A-48FE-A6A1-086A6116785D',50702,50702,507,'Examination',0,'Examination','出卷管理','BusinessCenter',GETDATE(),'admin',GETDATE(),'admin',0),




	-- 报表中心	
	('0BCF0797-E9A9-4DFE-9776-9E71E85ACCC9',6,6,0,'ReportCenter_Index',0,'ReportCenter_Index','报表中心','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 报表中心 子级菜单	
	--('4F7F6F05-FE9A-4BDE-BD11-F46F8DF9EA3E',601,601,6,'OrderReport',0,'OrderReport','订单管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('1982C1A2-4D04-4A33-9B99-F1CED3316FFE',602,602,6,'BusinessAnalysisReport',0,'BusinessAnalysisReport','业务分析报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('5A3D91B7-10D9-424E-93B5-ABA6F57E4D55',603,603,6,'ContactReport',0,'ContactReport','联系管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('F4D95A78-B7E7-4F77-B078-4129AA5FA872',604,604,6,'CustomerReportMgr_Index',0,'CustomerReportMgr_Index','客户管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 报表中心->客户管理报表 子级菜单	
	--('0A041525-05D2-49AA-9E07-BD50D56BF1C1',60401,60401,604,'CustomerConversionRate',0,'CustomerConversionRate','客户转化率报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	('BF8DE407-702C-4EEE-9988-A6984D5D847A',605,605,6,'WorkOrderReport_Index',0,'WorkOrderReport_Index','工单管理报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	('F26F047E-CC60-4264-9D95-DD95CB0A74D7',60501,60501,605,'WorkOrderStatisticsReport',0,'WorkOrderStatisticsReport','工单统计信息报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	--('931630F4-9A36-48E8-ACA9-848F6D06ED07',606,606,6,'ChargeOrderReport',0,'ChargeOrderReport','扣款报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('5B108601-404E-4D50-85EC-1BB8447C6850',607,607,6,'DeliveryQuery',0,'DeliveryQuery','物流综合查询','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('CF82A43E-2697-47C3-9948-7DE432711062',608,608,6,'OpeningOrderReport',0,'OpeningOrderReport','开户报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('F64CFD1B-4011-46B1-AE4B-57CB021607D7',609,609,6,'RecoverOrderReport',0,'RecoverOrderReport','回收报表','ReportCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 研发中心	
	--('6942D6AF-C679-4F85-AF0C-EDD426B3E58D',7,7,0,'CentreCenter_Index',0,'CentreCenter_Index','研发中心','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	---- 研发中心 子级菜单
	--('8CBEBF24-F34A-4C89-B656-26BDE9BAAD0E',701,701,7,'CentreProduct_Index',0,'CentreProduct_Index','产品视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	---- 研发中心->产品视图 子级菜单
	--('904E7FC7-A950-45E9-B01F-36009ED4DE9D',70101,70101,701,'ProductList',0,'CentreProduct_Index','产品列表','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('D6EC481D-513A-4FF1-8B67-CF974B4A51D8',70102,70102,701,'RequirementsMgr',0,'RequirementsMgr','需求管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('D51B80AB-1698-401E-A8B1-B57AA69C5F73',70103,70103,701,'PlanMgr',0,'PlanMgr','计划管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('17B1B6AD-9818-4F72-8022-D7A4706F6477',70104,70104,701,'ReleaseMgr',0,'ReleaseMgr','发布管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('0B027306-A74A-4654-8DFC-E56836A9C7EF',70105,70105,701,'Roadmap',0,'Roadmap','线路图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('446C8307-E262-4C05-A991-7A4D117B9A24',70106,70106,701,'ProductDoc',0,'ProductDoc','产品文档','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('A3E31FD7-CEED-4984-A617-46241641FABD',70107,70107,701,'ProductModule',0,'ProductModule','模块管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	--('6D50DB08-3913-4D12-B81D-EA2C093ED875',702,702,7,'CentreProject_Index',0,'CentreProject_Index','项目视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	---- 研发中心->项目视图 子级菜单
	--('02F7DE28-7679-4D67-A9D9-B5D7C27866D8',70201,70201,702,'ProjectRequirements',0,'ProjectRequirements','项目需求','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('D10C9DCD-7966-40B5-91F5-C2B22FB3BF4E',70202,70202,702,'ProjectTaskList',0,'ProjectTaskList','计划任务','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('C5657A50-2CD3-434A-9C21-24CBE66789DF',70203,70203,702,'Burndown',0,'Burndown','燃尽图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('5562B006-C55D-4353-9612-040634F67A52',70204,70204,702,'ProjectDoc',0,'ProjectDoc','项目文档','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('54E12361-4746-41A4-AB20-6735D1363212',70205,70205,702,'BugMgr',0,'BugMgr','BUG管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('FA7B6EF7-318A-42F5-9B81-9C959A2482FC',70206,70206,702,'ProjectRelease',0,'ProjectRelease','发布管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('FC9EE182-1E01-4A29-ACFA-4C03F2CBA29D',70207,70207,702,'ProjectTeam',0,'ProjectTeam','团队管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	--('6139CD75-FC8B-4FF3-9348-D6A65F0E64C4',703,703,7,'CentreTest_Index',0,'CentreTest_Index','测试视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	---- 研发中心->测试视图 子级菜单
	--('3B03B001-3514-4C81-81BE-9C159F9CA0C1',70301,70301,703,'BugMgr',0,'BugMgr','缺陷管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('4A336459-AF25-4AB9-BCEB-02B899A53A43',70302,70302,703,'TestCaseMgr',0,'TestCaseMgr','用例管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('CA49AD74-89CE-412D-AD9E-9DAD5F5F0AD3',70303,70303,703,'TestListMgr',0,'TestListMgr','测试任务','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	--('6B2FE570-242B-4686-B55C-B4570ACDE5F3',704,704,7,'CentreDocument_Index',0,'CentreDocument_Index','文档视图','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),

	---- 研发中心->文档视图 子级菜单
	--('CF995724-B4C9-4BF1-93CA-7457494BB5F7',70401,70401,704,'DocLibMgr',0,'DocLibMgr','文档库管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('FB37C5DB-A2D4-4DE8-A376-389D54B54F43',70402,70402,704,'DocList',0,'DocList','文档列表','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),
	--('187384A8-F2FD-4A83-9F27-BCBD80EF2228',70403,70403,704,'DocModule',0,'DocModule','模块管理','CentreCenter',GETDATE(),'admin',GETDATE(),'admin',0),


	-- 系统管理	
	('DC6FCE44-2AC9-49F9-A84E-C76379F975CC',8,8,0,'System_Index',0,'System_Index','系统管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),

	-- 系统管理	子级菜单
	('486319C1-07A6-4E68-A734-CABBC0708CC7',801,801,8,'UserList',0,'UserList','用户管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('90B724BA-8021-4D89-92BE-410D1134B80F',802,802,8,'DepList',0,'DepList','部门管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 系统管理->部门管理 包含操作列表
	('922BA177-C8E4-44D7-8FCB-C2DF190045B0',80201,80201,802,'DepUserList',1,'DepList','获取部门用户操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('F9041FD2-3812-463E-9580-241198FE6A99',80202,80202,802,'DoAddDepartmentUser',1,'DepList','添加部门成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('DC5F0DAD-3812-459D-BADA-4BF4333EC7BF',80203,80203,802,'DoRemoveDepartmentUser',1,'DepList','删除部门成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('D566CAC4-B0B5-4A14-B714-B4DF7588D0AB',80204,80204,802,'NewDepartment',1,'DepList','新建部门视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('DD7A0564-F784-4AC4-AC1B-E2A5A1F84FF2',80205,80205,802,'DoNewDepartment',1,'DepList','新建部门操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('81BD97A1-D121-4618-8F96-2089B4C1C77E',80206,80206,802,'EditDepartment',1,'DepList','编辑部门视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('C41710C1-6CB2-40C5-AA1E-D615F0D3D210',80207,80207,802,'DoUpdateDepartment',1,'DepList','更新部门操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('AE7F994F-C547-4EAA-85D0-B53E53FD6189',80208,80208,802,'DoDeleteDepartment',1,'DepList','删除部门操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('2CD10A1B-28ED-4F03-A2DD-CA035E8C8190',80209,80209,802,'AddDepUser',1,'DepList','添加部门用户视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	
	('7FB3FB22-57AA-4B28-ABB8-3BD6BEDA4C66',803,803,8,'Premission',0,'Premission','权限设置','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	
	-- 系统管理->权限设置 包含操作列表
	('4F7AD309-E6CF-43D9-80F8-7BAFD180DB49',80301,80301,803,'RoleUserList',1,'Premission','获取角色用户列表','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('DEA16DB4-D357-4C28-BED7-7EE36BDBE01D',80302,80302,803,'AddRoleUser',1,'Premission','新增角色用户视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('724737A1-A691-4056-8FE4-7EE7FF04ADBF',80303,80303,803,'NewRole',1,'Premission','新增角色视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('770A17A8-74D8-4CFA-9AAC-E76F9A5D6169',80304,80304,803,'EditRole',1,'Premission','编辑角色视图','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('4DF04A5E-28A0-492B-BD65-6241727D6B48',80305,80305,803,'DoNewRole',1,'Premission','新增角色操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('1B340E74-55F3-4781-A7BE-6B8161C3897B',80306,80306,803,'DoUpdateRole',1,'Premission','更新角色操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('09D083BF-6ECB-4CDF-82F6-477F8551D78E',80307,80307,803,'DoDeleteRole',1,'Premission','删除角色操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('E224A491-80E0-443E-AF7D-20E4C659F693',80308,80308,803,'DoAddRoleUserList',1,'Premission','添加角色成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('74D6CB1C-4EFD-4D86-8C60-EFE74E5CE2AD',80309,80309,803,'DoRemoveRoleUser',1,'Premission','移除角色成员操作','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),

	('5D9CC136-98B7-4ECD-BED0-8F4A634BAA5A',804,804,8,'SysLogs',0,'SysLogs','系统日志','System',GETDATE(),'admin',GETDATE(),'admin',0),
	('0E80210F-6967-4D12-9557-09C5D3FF61AD',805,805,8,'UserGroupMgr',0,'UserGroupMgr','用户组管理','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('4EEE9BFC-D35B-4D2B-8E80-F7EAC81AF8E1',80501,80501,805,'NewUserGroup',1,'UserGroupMgr','新建用户组','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('1F6B24EA-F4E9-498C-A5E3-33476CB00897',80502,80502,805,'GroupUserList',1,'UserGroupMgr','本组用户列表','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('19799623-F26C-42D7-8086-2D99AC1B2FDC',80503,80503,805,'AddUserGroupUser',1,'UserGroupMgr','添加本组用户','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('355A997C-1B9A-4F25-8F79-D9547AB03BEF',80504,80504,805,'EditUserGroup',1,'UserGroupMgr','编辑用户组信息','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('89D7D973-967C-448F-A631-24DB8D1B3259',80506,80506,805,'DoDeleteUserGroup',1,'UserGroupMgr','删除本组用户','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('49319F59-6FB4-49B5-A38C-39A08ADC7D4F',80507,80507,805,'DoSetGroupManagerUser',1,'UserGroupMgr','设定本组负责人','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('D5E3C03E-A12A-47A9-B41F-E34C95A7D0E0',80508,80508,805,'DoUpdateUserGroupInfo',1,'UserGroupMgr','更新用户组信息','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('CA6057E9-0613-47F2-BAC9-4C2B3F3A84ED',80509,80509,805,'DoRemoveGroupUser',1,'UserGroupMgr','移除组内用户','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('158B827F-1801-4F57-B53B-067974F2AA86',80510,80510,805,'DoAddGroupUser',1,'UserGroupMgr','添加用户组用户','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),
	('7248C2BD-7791-4ADE-9D79-AF46CB82774F',80511,80511,805,'DoNewUserGroupInfo',1,'UserGroupMgr','新建用户组','UserMgr',GETDATE(),'admin',GETDATE(),'admin',0),

	
	('7AE55462-7727-4F6D-8989-FE15CAF4DDC0',9,9,0,'Index',1,'Home_Index','首页','Home',GETDATE(),'UserMgr',GETDATE(),'admin',0);


GO



truncate table [user_info];

INSERT INTO [user_info]
	([user_id],[role_id],[work_id],		[cti_user_id],[cti_user_pwd],[cn_name],[en_name],[login_pwd],[login_name],		[user_email],[entry_date],[positive_date],[leave_date],[department_id],							[post_name],[team_name],	[work_status],							[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	(NEWID(),	'',		 'WORKID_1149',	'1149',		  '1149',		 '肖金',   '肖金',	 '1149',	 'LOGINNAME_1149',	null,		 null,		  null,			  null,			'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',	'',			'',				'DD1C5D4E-46F9-4535-8462-661534DC2A1A',	0, GETDATE(),'admin',null,'Admin',0),
	(NEWID(),	'',		 'WORKID_1155',	'1155',		  '1155',		 '黄碧莲', '黄碧莲', '1155',	 'LOGINNAME_1155',	null,		 null,		  null,			  null,			'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',	'',			'',				'DD1C5D4E-46F9-4535-8462-661534DC2A1A',	0, GETDATE(),'admin',null,'Admin',0),
	(NEWID(),	'',		 'WORKID_1153',	'1153',		  '1153',		 '徐婵',	'徐婵',  '1153',	 'LOGINNAME_1153',	null,		 null,		  null,			  null,			'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',	'',			'',				'DD1C5D4E-46F9-4535-8462-661534DC2A1A',	0, GETDATE(),'admin',null,'Admin',0),
	(NEWID(),	'',		 'WORKID_1157',	'1157',		  '1157',		 '谢雪梅',	'谢雪梅','1157',	 'LOGINNAME_1157',	null,		 null,		  null,			  null,			'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',	'',			'',				'DD1C5D4E-46F9-4535-8462-661534DC2A1A',	0, GETDATE(),'admin',null,'Admin',0),
	(NEWID(),	'',		 'WORKID_1160',	'1160',		  '1160',		 '孙晓男',	'孙晓男','1160',	 'LOGINNAME_1160',	null,		 null,		  null,			  null,			'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',	'',			'',				'DD1C5D4E-46F9-4535-8462-661534DC2A1A',	0, GETDATE(),'admin',null,'Admin',0),
	(NEWID(),	'',		 'WORKID_1161',	'1161',		  '1161',		 '李骥才',	'李骥才','1161',	 'LOGINNAME_1161',	null,		 null,		  null,			  null,			'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',	'',			'',				'DD1C5D4E-46F9-4535-8462-661534DC2A1A',	0, GETDATE(),'admin',null,'Admin',0);

go

--INSERT INTO [user_info]
--	([user_id],[role_id],[work_id],[cti_user_id], [cti_user_pwd], [cn_name],[en_name],[login_pwd],[login_name],[user_email],[entry_date],[positive_date],[leave_date],[department_id],[post_name],[team_name],[work_status],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
--select 
--	u.UserId,'','WORKID_' + u.UserName,u.UserName,u.UserName, u.RealName,u.RealName,u.Password,'LOGINNAME_' + u.UserName,null,null,null,null,'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55',u.Question,u.Answer,'DD1C5D4E-46F9-4535-8462-661534DC2A1A',0, GETDATE(),'admin',null,UserID,0
--from 
--	txms_db_201206282300.dbo.lrs_Users u
--GO

delete from user_info where user_id = 'C792D747-6B74-4A58-BB5B-D98EF420F99F';

INSERT INTO [user_info]
	([user_id],[role_id],[work_id],[cti_user_id],[cti_user_pwd],[cn_name],[en_name],[login_pwd],[login_name],[user_email],[entry_date],[positive_date],[leave_date],[department_id],[post_name],[team_name],[work_status],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES 
	('C792D747-6B74-4A58-BB5B-D98EF420F99F','656AECDA-717F-4CB8-A9A9-E7D0C75E82CF','WORKID_8888','1013','1013','管理员','admin','admin','LOGINNAME_8888',null,null,null,null,'AF4351C5-6F6A-45BB-8FD9-34DB699AEF55','','','DD1C5D4E-46F9-4535-8462-661534DC2A1A',0, GETDATE(),'admin',null,'Admin',0)


truncate table rel_role_action;

insert into rel_role_action (role_id, action_id, created_on, created_by, modified_on,modified_by,status_code)
select '656AECDA-717F-4CB8-A9A9-E7D0C75E82CF',action_id, GETDATE(),'admin',null,null,0 from action_info;

go

-- 更新旧密码为工号
update [user_info] set login_pwd = cti_user_id where LEN(login_pwd) > 20;

-- truncate table rel_user_group;

GO

truncate table [workorder_type_info];

INSERT INTO [workorder_type_info]
           ([workorder_type_id],[type_name],[description],[workorder_type_status],[sort_order],[created_on],[created_by],[status_code])
SELECT
		   workorder_type_id,[type_name],[description],workorder_type_status,sort_order,getdate(),'Admin',0
FROM 
	[txms_db_201206282300].dbo.workorder_type_info;

GO		

truncate table [workorder_status_info];

INSERT INTO [workorder_status_info]
           ([workorder_status_id],[workorder_type_id],[status_name],[description],[status_tag],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
SELECT 
	[workorder_status_id],[workorder_type_id],[status_name],[description],0,0,0,[created_on],[created_by],[modified_on],[modified_by],[status_code]
FROM 
	[txms_db_201206282300].[dbo].[workorder_status_info];


GO

truncate table [workorder_result_info];

INSERT INTO [workorder_result_info]
           ([workorder_result_id],[workorder_type_id],[result_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
SELECT 
		[workorder_result_id],[workorder_type_id],[result_name],[description],0,[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code]
FROM 
  [txms_db_201206282300].[dbo].[workorder_result_info];
  
GO

truncate table [user_group_info];

INSERT INTO [user_group_info]
           ([user_group_id],[group_name],[description],[created_on],[created_by],[modified_on],[modified_by],[status_code])
SELECT 
		[user_group_id],[group_name],[description],[created_on],[created_by],[modified_on],[modified_by],[status_code]
FROM 
  [txms_db_201206282300].[dbo].[user_group_info];
GO


insert into [user_group_info]
	(user_group_id, group_name, description, created_on, created_by, modified_by, modified_on, status_code)
values
	('0A63F2FC-5483-4F20-88B6-274235D979E8', '77项目组','77项目组', GETDATE(),'admin',null,null,0);
	
insert into [rel_user_group]
	(user_id, group_id, user_status_in_group, role_in_group, created_by,created_on, modified_by, modified_on, status_code)
select 
	user_id, '0A63F2FC-5483-4F20-88B6-274235D979E8', 0,null, 'C792D747-6B74-4A58-BB5B-D98EF420F99F', GETDATE(),null,null, 0
from 
	user_info 
where cn_name in 
('刘白丽','闫瑶','林泽刁','王双','陈春燕','张智杰','肖金','黄碧莲','徐婵','谢雪梅','孙晓男','李骥才');


update 
	[user_info] 
set 
	role_id = 'fd0aea4f-1ca0-42da-a25f-243dc10aaee5'
where 
	cn_name in 
	('刘白丽','闫瑶','林泽刁','王双','陈春燕','张智杰','肖金','黄碧莲','徐婵','谢雪梅','孙晓男','李骥才');


truncate table [product_category_group_info];

INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('087C4A39-94C8-4CCB-8071-09D1E75594C8','移动电源','移动电源',1,1,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('261A2002-B77F-46F8-9474-2E86921DED2A','智能手机终端','智能手机终端',1,2,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('2A255058-7277-4FB3-BFBC-8D69C4245C48','贴膜','贴膜',1,3,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('30D52946-A29A-4127-A7FE-39A0D6206BF6','电信话费套餐','电信话费套餐',1,4,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('596F611F-53FB-48E0-8D1C-B62FD1E8C834','读卡器','读卡器',1,5,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('6B8549D2-8425-4C2E-A53A-67D1DAF27117','充电器','充电器',1,6,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('8C258624-2B4F-4C1C-B095-8C727856A84D','电话机及配件','电话机及配件',1,11,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('BBC2E8AD-6C43-40F4-87C7-FD4C6DB9A2FB','手机饰品','手机饰品',1,7,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('D4F2D04C-2184-4AC7-86C3-30A4BFD55176','手机外壳','手机外壳',1,8,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('D805D68C-C238-4C89-B042-0EAC01869E7D','电信手机号码','电信手机号码',1,9,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);
INSERT INTO [product_category_group_info] ([product_category_group_id],[group_name],[description],[is_item_price],[sort_order],[status],[created_on],[created_by],[modified_on],[modified_by],[status_code]) VALUES ('F8FB9660-5DD5-42D1-BFB4-5513B4CBF957','内存卡','内存卡',1,10,0,getdate(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);


update product_category_info set group_name = g.product_category_group_id
from [product_category_group_info] g where g.group_name = product_category_info.group_name;


declare @productCategoryId varchar(50);
select @productCategoryId = product_category_id from product_category_info where category_name = '电信手机号码';

INSERT INTO [product_category_sales_status]
           ([sales_status_id],[product_category_id],[salestatus_name],[description],[status],[sort_order],[created_on],[created_by],[modified_on],[modified_by],[status_code])
     VALUES
           ('D4928A32-8AFA-4D52-892A-F51F077ADF2E',@productCategoryId,'已开放','已开放',0,3,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
           ('615FCF85-70E6-4CE4-86BE-D7800FB34C44',@productCategoryId,'已分配','已分配',0,4,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
           ('46566A63-7502-4521-8A58-194A3B65B247',@productCategoryId,'已锁定','已锁定',0,5,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0),
           ('2508DE06-1581-4D63-91FB-DE92F54FF7E0',@productCategoryId,'已销售','已销售',0,6,GETDATE(),'C792D747-6B74-4A58-BB5B-D98EF420F99F',null,null,0);



declare @typeid varchar(50);

select @typeid = workorder_type_id from workorder_type_info where type_name = '售后工单';

update workorder_type_info set parent_id = @typeid where type_name in
('银行类售后','综合(其他)售后','终端售后','电信类售后','物流类售后');

select @typeid = workorder_type_id from workorder_type_info where type_name = '客户关怀';

update workorder_type_info set parent_id = @typeid where type_name in
('WelComeCall','节日问候','托收激活','生日关怀','拆机维挽','停机维挽');

select @typeid = workorder_type_id from workorder_type_info where type_name = '营销工单';

update workorder_type_info set parent_id = @typeid where type_name in
('Nokia800预订','广州IP4S自动外呼','IP4s预定','计划换机','华为荣耀','同号营销','小米预定','SZ11126OB','梅州IP4S销售','重温旧梦','深圳IP4S自动外呼','未知预定');

select @typeid = workorder_type_id from workorder_type_info where type_name = '内部工单';

update workorder_type_info set parent_id = @typeid where type_name in
('促销申请');


select @typeid = workorder_type_id from workorder_type_info where type_name = '家校通项目';

update workorder_type_info set parent_id = @typeid where type_name in
('60项目JM','68项目（02）','68项目（03）','68项目（04）');


