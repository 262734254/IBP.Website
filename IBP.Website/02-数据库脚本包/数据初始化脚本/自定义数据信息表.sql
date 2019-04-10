-- exec lrs_CodeNameGetByTypeNo @CodeType=0

truncate table ibp_db.dbo.custom_data_info;
truncate table ibp_db.dbo.custom_data_value;

DECLARE @custom_data_id varchar(50);
select @custom_data_id = newid();

-- 客户等级 ----------------------------------------------------------
INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'客户等级',Null,'custom_data','customer_level','string',2,20,1,0,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 0;


-- 客户来源 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'客户来源',Null,'custom_data','customer_level','string',2,20,1,1,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 1;


-- 工单类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'工单类型',Null,'custom_data','customer_level','string',2,20,1,2,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 2;


-- 工单级别 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'工单级别',Null,'custom_data','customer_level','string',2,20,1,3,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 3;
	
	

-- 证件类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'证件类型',Null,'custom_data','customer_level','string',2,20,1,4,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 4;
	
	
	
-- 运营商 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'运营商',Null,'custom_data','customer_level','string',2,20,1,5,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 5;
	
-- 开卡行 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'开卡行',Null,'custom_data','customer_level','string',2,20,1,6,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 6;
	
-- 开卡城市 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'开卡城市',Null,'custom_data','customer_level','string',2,20,1,7,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 7;

-- 托收银行 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'托收银行',Null,'custom_data','customer_level','string',2,20,1,8,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 8;	
		

-- 审批异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'审批异常类型',Null,'custom_data','customer_level','string',2,20,1,9,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 10;	

-- 开卡异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'开卡异常类型',Null,'custom_data','customer_level','string',2,20,1,10,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 11;	

-- 备货异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'备货异常类型',Null,'custom_data','customer_level','string',2,20,1,11,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 12;				
			
-- 发货异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'发货异常类型',Null,'custom_data','customer_level','string',2,20,1,12,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 13;		
				
-- 签收异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'签收异常类型',Null,'custom_data','customer_level','string',2,20,1,13,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 14;		
				
-- 回收异常类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'回收异常类型',Null,'custom_data','customer_level','string',2,20,1,14,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 15;					

-- 配送公司 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'配送公司',Null,'custom_data','customer_level','string',2,20,1,15,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 16;		
					
-- 广播类型 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'广播类型',Null,'custom_data','customer_level','string',2,20,1,16,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 17;	
	
	
-- 订单撤消原因 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'订单撤消原因',Null,'custom_data','customer_level','string',2,20,1,17,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 18;	
	
-- 销售撤消原因 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'销售撤消原因',Null,'custom_data','customer_level','string',2,20,1,18,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 19;	
	
-- 号码级别 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'号码级别',Null,'custom_data','customer_level','string',2,20,1,19,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 20;	
		
-- 订单失败原因 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'订单失败原因',Null,'custom_data','customer_level','string',2,20,1,20,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 21;		


-- 订单来源 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'订单来源',Null,'custom_data','customer_level','string',2,20,1,21,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 22;	

-- 通讯消费 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'通讯消费',Null,'custom_data','customer_level','string',2,20,1,22,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 24;	
								

-- 优选品牌 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'优选品牌',Null,'custom_data','customer_level','string',2,20,1,23,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 25;	
	
-- 手机价位 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'手机价位',Null,'custom_data','customer_level','string',2,20,1,24,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 26;	
		
-- 销售城市 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'销售城市',Null,'custom_data','customer_level','string',2,20,1,25,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
select 
	NEWID(),@custom_data_id,name, code,showsort,isshow,GETDATE(),'admin',null,null,0
from
	Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 27;	
	

-- 工作状态 ----------------------------------------------------------

select @custom_data_id = newid();

INSERT INTO ibp_db.dbo.custom_data_info
	([data_id],[data_name],[data_code],[data_type],[field_name],[field_type],[min_length],[max_length],[requested],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
VALUES
	(@custom_data_id,'工作状态',Null,'custom_data','customer_level','string',2,20,1,26,0,GETDATE(),'admin',GETDATE(),null,0)

INSERT INTO [ibp_db].[dbo].[custom_data_value]
	([value_id],[data_id],[data_value],[data_value_code],[sort_order],status,[created_on],[created_by],[modified_on],[modified_by],[status_code])
values 
	('8FA195F5-21B4-45D8-8472-23E013B336BA',@custom_data_id,'试用','WorkStatus001',0,0,GETDATE(),'admin',null,null,0),
	('DD1C5D4E-46F9-4535-8462-661534DC2A1A',@custom_data_id,'转正','WorkStatus002',1,0,GETDATE(),'admin',null,null,0),
	('2CA85927-4E1E-408A-8E44-A3A24F1046D5',@custom_data_id,'离职','WorkStatus003',2,0,GETDATE(),'admin',null,null,0);
											
select c.data_name, v.* from ibp_db.dbo.custom_data_value v inner join ibp_db.dbo.custom_data_info c on v.data_id = C.data_id where v.data_id = @custom_data_id order by v.sort_order 


GO

-- select NEWID()

-- select * from Test_TXMS_20111129.dbo.sys_CodeType where CodeType = 0;